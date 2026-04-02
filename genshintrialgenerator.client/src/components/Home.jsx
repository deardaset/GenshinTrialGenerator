import { useState, useEffect, useCallback } from 'react';
import { getHero } from '../api/heroApi';
import { getBoss } from '../api/bossApi';
import '../css/Home.css';

const HISTORY_KEY = 'genshin_trial_history';
const HISTORY_LIMIT = 5;

function pickRandom(arr, count) {
  const shuffled = [...arr].sort(() => Math.random() - 0.5);
  return shuffled.slice(0, count);
}

function saveToHistory(trial) {
  const prev = JSON.parse(localStorage.getItem(HISTORY_KEY) || '[]');
  const next = [trial, ...prev].slice(0, HISTORY_LIMIT);
  localStorage.setItem(HISTORY_KEY, JSON.stringify(next));
  return next;
}

function loadHistory() {
  return JSON.parse(localStorage.getItem(HISTORY_KEY) || '[]');
}

// ── Hero card (small) ──────────────────────────────────────────
function HeroSlot({ hero, index }) {
  return (
    <div className="hero-slot" style={{ '--delay': `${index * 0.08}s` }}>
      <div className="hero-slot__img-wrap">
        {hero.photoUrl
          ? <img src={hero.photoUrl} alt={hero.name} />
          : <div className="hero-slot__placeholder">{hero.name?.[0]}</div>
        }
        <div className="hero-slot__shimmer" />
      </div>
      <p className="hero-slot__name">{hero.name}</p>
    </div>
  );
}

// ── Boss card ──────────────────────────────────────────────────
function BossSlot({ boss }) {
  return (
    <div className="boss-slot">
      <div className="boss-slot__img-wrap">
        {boss.photoUrl
          ? <img src={boss.photoUrl} alt={boss.name} />
          : <div className="boss-slot__placeholder">{boss.name?.[0]}</div>
        }
        <div className="boss-slot__overlay" />
        <div className="boss-slot__label">Противник</div>
      </div>
      <div className="boss-slot__info">
        <p className="boss-slot__name">{boss.name}</p>
        {boss.damageType && (
          <span className={`boss-slot__element el-tag--${boss.damageType?.toLowerCase()}`}>
            {boss.damageType}
          </span>
        )}
      </div>
    </div>
  );
}

// ── History entry ──────────────────────────────────────────────
function HistoryEntry({ trial, index }) {
  return (
    <div className="history-entry">
      <div className="history-entry__head">
        <span className="history-entry__num">#{index + 1}</span>
        <span className="history-entry__date">{trial.date}</span>
        <span className="history-entry__boss">{trial.boss.name}</span>
      </div>
      <div className="history-entry__heroes">
        {trial.heroes.map(h => (
          <div key={h.guid} className="history-hero">
            {h.photoUrl
              ? <img src={h.photoUrl} alt={h.name} />
              : <div className="history-hero__placeholder">{h.name?.[0]}</div>
            }
            <span>{h.name}</span>
          </div>
        ))}
      </div>
    </div>
  );
}

// ── Main component ─────────────────────────────────────────────
export default function Home() {
  const [allHeroes, setAllHeroes] = useState([]);
  const [allBosses, setAllBosses] = useState([]);
  const [dataLoaded, setDataLoaded] = useState(false);
  const [dataError, setDataError]   = useState(false);

  const [trial, setTrial]     = useState(null); // { heroes, boss }
  const [history, setHistory] = useState(loadHistory);
  const [spinning, setSpinning] = useState(false);

  // ── Load all heroes and bosses once ─────────────────────────
  useEffect(() => {
    async function fetchAll() {
      try {
        const [heroData, bossData] = await Promise.all([
          getHero({ page: 1, pageSize: 999 }),
          getBoss({ page: 1, pageSize: 999 }),
        ]);
        setAllHeroes(heroData.items ?? heroData);
        setAllBosses(bossData.items ?? bossData);
        setDataLoaded(true);
      } catch {
        setDataError(true);
      }
    }
    fetchAll();
  }, []);

  // ── Generate trial ───────────────────────────────────────────
  const generate = useCallback(() => {
    if (allHeroes.length < 4 || allBosses.length < 1) return;

    setSpinning(true);
    setTimeout(() => {
      const heroes = pickRandom(allHeroes, 4);
      const [boss]  = pickRandom(allBosses, 1);
      const newTrial = {
        heroes,
        boss,
        date: new Date().toLocaleDateString('ru-RU', {
          day: '2-digit', month: '2-digit', year: 'numeric',
          hour: '2-digit', minute: '2-digit',
        }),
      };
      setTrial(newTrial);
      setHistory(saveToHistory(newTrial));
      setSpinning(false);
    }, 600);
  }, [allHeroes, allBosses]);

  // Auto-generate on first load once data is ready
  useEffect(() => {
    if (dataLoaded) generate();
  }, [dataLoaded]); // eslint-disable-line react-hooks/exhaustive-deps

  const canGenerate = dataLoaded && allHeroes.length >= 4 && allBosses.length >= 1;

  return (
    <div className="home">
      {/* ── Hero section ── */}
      <div className="home__hero-section">
        <div className="home__hero-bg" aria-hidden="true">
          {[...Array(6)].map((_, i) => <span key={i} className={`orb orb-${i + 1}`} />)}
        </div>
        <div className="home__hero-content">
          <p className="home__eyebrow">Genshin Trial Generator</p>
          <h1 className="home__title">
            Испытай<br />
            <span className="home__title-accent">свой отряд</span>
          </h1>
          <p className="home__subtitle">
            Рандомный отряд из 4 героев против босса — принимаешь вызов?
          </p>
        </div>
      </div>

      {/* ── Generator ── */}
      <div className="generator">

        {/* Error */}
        {dataError && (
          <div className="generator__error">
            Не удалось загрузить данные. Проверь соединение с сервером.
          </div>
        )}

        {/* Loading skeleton */}
        {!dataLoaded && !dataError && (
          <div className={`trial-board trial-board--skeleton`}>
            <div className="trial-team">
              {[...Array(4)].map((_, i) => (
                <div key={i} className="hero-slot hero-slot--skeleton">
                  <div className="hero-slot__img-wrap skeleton-box" />
                  <div className="skeleton-line" />
                </div>
              ))}
            </div>
            <div className="trial-vs"><span>VS</span></div>
            <div className="boss-slot boss-slot--skeleton">
              <div className="boss-slot__img-wrap skeleton-box" />
            </div>
          </div>
        )}

        {/* Trial board */}
        {trial && (
          <div className={`trial-board ${spinning ? 'trial-board--spinning' : 'trial-board--visible'}`}>
            {/* Team */}
            <div className="trial-team">
              {trial.heroes.map((hero, i) => (
                <HeroSlot key={hero.guid} hero={hero} index={i} />
              ))}
            </div>

            {/* VS divider */}
            <div className="trial-vs">
              <div className="trial-vs__line" />
              <span>VS</span>
              <div className="trial-vs__line" />
            </div>

            {/* Boss */}
            <BossSlot boss={trial.boss} />
          </div>
        )}

        {/* Generate button */}
        {!dataError && (
          <button
            className={`generate-btn ${spinning ? 'generate-btn--spinning' : ''}`}
            onClick={generate}
            disabled={!canGenerate || spinning}
          >
            {!dataLoaded
              ? 'Загрузка...'
              : spinning
              ? 'Жеребьёвка...'
              : trial
              ? '✦ Новое испытание'
              : '✦ Сгенерировать испытание'
            }
          </button>
        )}

        {canGenerate && (
          <p className="generator__hint">
            {allHeroes.length} героев · {allBosses.length} боссов в базе
          </p>
        )}
      </div>

      {/* ── History ── */}
      {history.length > 0 && (
        <div className="history">
          <div className="history__head">
            <h2 className="history__title">История испытаний</h2>
            <button
              className="history__clear"
              onClick={() => {
                localStorage.removeItem(HISTORY_KEY);
                setHistory([]);
              }}
            >
              Очистить
            </button>
          </div>
          <div className="history__list">
            {history.map((t, i) => (
              <HistoryEntry key={t.date + i} trial={t} index={i} />
            ))}
          </div>
        </div>
      )}
    </div>
  );
}