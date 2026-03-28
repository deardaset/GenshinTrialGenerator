import { useState, useEffect, useCallback } from 'react';
import { getHero, createHero, updateHero, deleteHero } from '../api/heroApi';
import EntityCard from './EntityCard';
import EntityPanel from './EntityPanel';
import '../css/EntityPage.css';

const ELEMENTS = ['', 'Pyro', 'Hydro', 'Anemo', 'Electro', 'Dendro', 'Cryo', 'Geo'];

export default function Heroes() {
  // ── Data state ──────────────────────────────────────────────
  const [heroes, setHeroes]     = useState([]);
  const [total, setTotal]       = useState(0);
  const [loading, setLoading]   = useState(true);
  const [fetchErr, setFetchErr] = useState(null);

  // ── Filters ─────────────────────────────────────────────────
  const [search, setSearch]   = useState('');
  const [element, setElement] = useState('');
  const [sortBy, setSortBy]   = useState('');
  const [page, setPage]       = useState(1);
  const PAGE_SIZE = 12;

  // ── Panel state ──────────────────────────────────────────────
  const [panelOpen, setPanelOpen]   = useState(false);
  const [panelMode, setPanelMode]   = useState('detail'); // 'detail' | 'create' | 'edit'
  const [selected, setSelected]     = useState(null);
  const [saving, setSaving]         = useState(false);
  const [deleting, setDeleting]     = useState(false);
  const [formErrors, setFormErrors] = useState([]);

  // ── Fetch ────────────────────────────────────────────────────
  const load = useCallback(async () => {
    setLoading(true);
    setFetchErr(null);
    try {
      const data = await getHero({
        page,
        pageSize: PAGE_SIZE,
        sort:   sortBy || undefined,
        search: search || undefined,
        element: element || undefined
      });
      setHeroes(data.items ?? data);
      setTotal(data.total  ?? (data.items ?? data).length);
    } catch {
      setFetchErr('Не удалось загрузить героев.');
    } finally {
      setLoading(false);
    }
  }, [page, sortBy, search]);

  useEffect(() => { load(); }, [load]);
  useEffect(() => { setPage(1); }, [search, element, sortBy]);

  // ── Panel helpers ────────────────────────────────────────────
  function openDetail(hero) {
    setSelected(hero);
    setPanelMode('detail');
    setFormErrors([]);
    setPanelOpen(true);
  }

  function openCreate() {
    setSelected(null);
    setPanelMode('create');
    setFormErrors([]);
    setPanelOpen(true);
  }

  function closePanel() {
    setPanelOpen(false);
  }

  // ── CRUD ─────────────────────────────────────────────────────
  async function handleCreate(formData) {
    setSaving(true);
    setFormErrors([]);
    try {
      const created = await createHero(formData);
      setHeroes(prev => [created, ...prev]);
      closePanel();
    } catch (err) {
      setFormErrors(err.messages ?? ['Ошибка при создании.']);
    } finally {
      setSaving(false);
    }
  }

  async function handleUpdate(guid, formData) {
    setSaving(true);
    setFormErrors([]);
    try {
      const updated = await updateHero(guid, formData);
      setHeroes(prev => prev.map(h => h.guid === guid ? updated : h));
      setSelected(updated);
      setPanelMode('detail');
    } catch (err) {
      setFormErrors(err.messages ?? ['Ошибка при обновлении.']);
    } finally {
      setSaving(false);
    }
  }

  async function handleDelete() {
    if (!selected) return;
    setDeleting(true);
    try {
      await deleteHero(selected.guid);
      setHeroes(prev => prev.filter(h => h.guid !== selected.guid));
      closePanel();
    } catch {
      // stay open, let user retry
    } finally {
      setDeleting(false);
    }
  }

  const totalPages = Math.ceil(total / PAGE_SIZE);

  return (
    <div className="page-container">
      {/* ── Page hero ── */}
      <div className="page-hero">
        <div className="page-hero__left">
          <h1 className="page-hero__title">Герои</h1>
          <p className="page-hero__subtitle">Персонажи · {total} найдено</p>
        </div>
        <button className="btn-add" onClick={openCreate}>
          <span>+</span> Добавить героя
        </button>
      </div>

      {/* ── Toolbar ── */}
      <div className="toolbar">
        <div className="toolbar__search">
          <span className="toolbar__search-icon">⌕</span>
          <input
            className="toolbar__input"
            placeholder="Поиск по имени..."
            value={search}
            onChange={e => setSearch(e.target.value)}
          />
          {search && (
            <button className="toolbar__clear" onClick={() => setSearch('')}>✕</button>
          )}
        </div>

        <select
          className="toolbar__select"
          value={element}
          onChange={e => setElement(e.target.value)}
        >
          <option value="">Все элементы</option>
          {ELEMENTS.filter(Boolean).map(el => (
            <option key={el} value={el}>{el}</option>
          ))}
        </select>

        <select
          className="toolbar__select"
          value={sortBy}
          onChange={e => setSortBy(e.target.value)}
        >
          <option value="">По умолчанию</option>
          <option value="name">По имени</option>
          <option value="rarity">По редкости</option>
        </select>
      </div>

      {/* ── States ── */}
      {loading && (
        <div className="page-state">
          <div className="spinner" />
          <p>Загрузка героев...</p>
        </div>
      )}

      {fetchErr && !loading && (
        <div className="page-state page-state--error">
          <p>{fetchErr}</p>
          <button className="btn-ghost" onClick={load}>Повторить</button>
        </div>
      )}

      {!loading && !fetchErr && displayed.length === 0 && (
        <div className="page-state">
          <p className="page-state__title">Герои не найдены</p>
          <p>Попробуй изменить параметры поиска.</p>
        </div>
      )}

      {/* ── Grid ── */}
      {!loading && !fetchErr && heroes.length > 0 && (
        <div className="cards-grid">
          {heroes.map(hero => (
            <EntityCard key={hero.guid} entity={hero} onClick={openDetail} />
          ))}
        </div>
      )}

      {/* ── Pagination ── */}
      {totalPages > 1 && (
        <div className="pagination">
          <button
            className="pagination__btn"
            disabled={page === 1}
            onClick={() => setPage(p => p - 1)}
          >‹ Назад</button>

          <div className="pagination__pages">
            {Array.from({ length: totalPages }, (_, i) => i + 1)
              .filter(n => n === 1 || n === totalPages || Math.abs(n - page) <= 1)
              .reduce((acc, n, idx, arr) => {
                if (idx > 0 && n - arr[idx - 1] > 1) acc.push('…');
                acc.push(n);
                return acc;
              }, [])
              .map((n, i) =>
                n === '…'
                  ? <span key={`ellipsis-${i}`} className="pagination__ellipsis">…</span>
                  : <button
                      key={n}
                      className={`pagination__btn ${page === n ? 'active' : ''}`}
                      onClick={() => setPage(n)}
                    >{n}</button>
              )
            }
          </div>

          <button
            className="pagination__btn"
            disabled={page === totalPages}
            onClick={() => setPage(p => p + 1)}
          >Вперёд ›</button>
        </div>
      )}

      {/* ── Slide panel ── */}
      <EntityPanel
        open={panelOpen}
        mode={panelMode}
        entity={selected}
        onClose={closePanel}
        onCreate={handleCreate}
        onUpdate={handleUpdate}
        onDelete={handleDelete}
        onSwitchEdit={() => setPanelMode('edit')}
        loading={saving}
        deleting={deleting}
        errors={formErrors}
        label="Героя"
      />
    </div>
  );
}