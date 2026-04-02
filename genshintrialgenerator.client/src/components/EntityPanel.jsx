import { useState, useEffect } from 'react';
import '../css/EntityPanel.css';

// ── Enum dictionaries ──────────────────────────────────────────

const ELEMENTS = [
  { value: 'Pyro',    label: 'Пиро' },
  { value: 'Hydro',   label: 'Гидро' },
  { value: 'Anemo',   label: 'Анемо' },
  { value: 'Electro', label: 'Электро' },
  { value: 'Dendro',  label: 'Дендро' },
  { value: 'Cryo',    label: 'Крио' },
  { value: 'Geo',     label: 'Гео' },
];

const HERO_RARITY = [
  { value: 'FourStar', label: '4★' },
  { value: 'FiveStar', label: '5★' },
];

const HERO_WEAPON = [
  { value: 'Swords',    label: 'Меч' },
  { value: 'Claymores', label: 'Двуручник' },
  { value: 'Polearms',  label: 'Копьё' },
  { value: 'Bows',      label: 'Лук' },
  { value: 'Catalysts', label: 'Катализатор' },
];

const HERO_MODEL = [
  { value: 'TallMaleModel',     label: 'Высокий М' },
  { value: 'TallFemaleModel',   label: 'Высокая Ж' },
  { value: 'MediumMaleModel',   label: 'Средний М' },
  { value: 'MediumFemaleModel', label: 'Средняя Ж' },
  { value: 'ShortFemaleModel',        label: 'Низкий Ж' },
];

const HERO_TEAM_BONUS = [
  { value: '',                   label: 'Нет' },
  { value: 'ElementalResonance', label: 'Эллементальный резонанс' },
  { value: 'Moonsign',           label: 'Мунсайн' },
  { value: 'Hexerei',            label: 'Хексерей' },
];

const HERO_ROLES = [
  { value: 'OnField',        label: 'ОнФилд' },
  { value: 'OffField',       label: 'ОффФилд' },
  { value: 'DPS',            label: 'ДПС' },
  { value: 'Support',        label: 'Саппорт' },
  { value: 'Survivability',  label: 'Выживаемость' },
];

const BOSS_TYPES = [
  { value: 'Normal', label: 'Обычный' },
  { value: 'Weekly', label: 'Недельный' },
  { value: 'Event',  label: 'Ивентовый' },
  { value: 'Quest',  label: 'Квестовый' },
];

const BOSS_CATEGORIES = [
  { value: 'Hypostasis', label: 'Гипостазис' },
  { value: 'Regisvine',  label: 'Регизвин' },
  { value: 'Dragon',     label: 'Дракон' },
  { value: 'Machine',    label: 'Машина' },
  { value: 'Human',      label: 'Человек' },
  { value: 'Monster',    label: 'Монстр'}
];

const REGIONS = [
  { value: 'Mondstadt', label: 'Мондштадт' },
  { value: 'Liyue',     label: 'Ли Юэ' },
  { value: 'Inazuma',   label: 'Инадзума' },
  { value: 'Sumeru',    label: 'Сумеру' },
  { value: 'Fontaine',  label: 'Фонтейн' },
  { value: 'Natlan',    label: 'Натлан' },
  { value: 'Snezhnaya', label: 'Снежная' },
];

// ── Shared UI primitives ───────────────────────────────────────

function Field({ label, children }) {
  return (
    <div className="entity-form__field">
      <label className="entity-form__label">{label}</label>
      {children}
    </div>
  );
}

function ChipGroup({ options, value, onChange }) {
  return (
    <div className="chip-group">
      {options.map(o => (
        <button
          key={o.value}
          type="button"
          className={`chip ${o.color ? `chip--${o.color}` : ''} ${value === o.value ? 'chip--active' : ''}`}
          onClick={() => onChange(o.value)}
        >
          {o.label}
        </button>
      ))}
    </div>
  );
}

function Toggle({ value, onChange, labelOn, labelOff }) {
  return (
    <button
      type="button"
      className={`toggle-btn ${value ? 'toggle-btn--on' : ''}`}
      onClick={() => onChange(!value)}
    >
      <span className="toggle-btn__track">
        <span className="toggle-btn__thumb" />
      </span>
      <span className="toggle-btn__label">{value ? labelOn : labelOff}</span>
    </button>
  );
}

function ImageUpload({ preview, onChange }) {
  return (
    <label className="entity-form__img-upload">
      <input type="file" accept="image/*" onChange={onChange} hidden />
      {preview
        ? <img src={preview} alt="preview" className="entity-form__img-preview" />
        : <div className="entity-form__img-placeholder">
            <span>+</span>
            <p>Загрузить изображение</p>
          </div>
      }
      <div className="entity-form__img-overlay">Изменить</div>
    </label>
  );
}

function DetailRow({ label, value }) {
  if (value === null || value === undefined || value === '') return null;
  return (
    <div className="detail-row">
      <span className="detail-row__label">{label}</span>
      <span className="detail-row__value">{String(value)}</span>
    </div>
  );
}

// ── Hero Form ──────────────────────────────────────────────────

function HeroForm({ initial = {}, onSubmit, onCancel, loading, errors, label }) {
  const [form, setForm] = useState({
    name:        initial.name        ?? '',
    description: initial.description ?? '',
    element:     initial.element     ?? '',
    rarity:      initial.rarity      ?? 'FourStar',
    weapon:      initial.weapon      ?? '',
    model:       initial.model       ?? '',
    teamBonus:   initial.teamBonus   ?? '',
    role:        initial.role        ?? '',
    photo:       null,
  });
  const [preview, setPreview] = useState(initial.photoUrl ?? null);

  const set = (field, value) => setForm(f => ({ ...f, [field]: value }));

  function handleImage(e) {
    const file = e.target.files[0];
    if (!file) return;
    set('photo', file);
    setPreview(URL.createObjectURL(file));
  }

  function handleSubmit(e) {
    e.preventDefault();
    const fd = new FormData();
    Object.entries(form).forEach(([k, v]) => {
      if (k === 'photo') { if (v) fd.append('photo', v); }
      else fd.append(k, v);
    });
    onSubmit(fd);
  }

  return (
    <form className="entity-form" onSubmit={handleSubmit}>
      <ImageUpload preview={preview} onChange={handleImage} />

      <Field label="Имя">
        <input
          className="entity-form__input"
          value={form.name}
          onChange={e => set('name', e.target.value)}
          placeholder="Имя персонажа..."
          required
        />
      </Field>

      <Field label="Элемент">
        <ChipGroup
          options={ELEMENTS.map(e => ({ ...e, color: e.value.toLowerCase() }))}
          value={form.element}
          onChange={v => set('element', v)}
        />
      </Field>

      <Field label="Редкость">
        <ChipGroup options={HERO_RARITY} value={form.rarity} onChange={v => set('rarity', v)} />
      </Field>

      <Field label="Оружие">
        <ChipGroup options={HERO_WEAPON} value={form.weapon} onChange={v => set('weapon', v)} />
      </Field>

      <Field label="Роль">
        <ChipGroup options={HERO_ROLES} value={form.role} onChange={v => set('role', v)} />
      </Field>

      <Field label="Модель">
        <ChipGroup options={HERO_MODEL} value={form.model} onChange={v => set('model', v)} />
      </Field>

      <Field label="Командный бонус">
        <ChipGroup options={HERO_TEAM_BONUS} value={form.teamBonus} onChange={v => set('teamBonus', v)} />
      </Field>

      <Field label="Описание">
        <textarea
          className="entity-form__input entity-form__textarea"
          value={form.description}
          onChange={e => set('description', e.target.value)}
          placeholder="Описание персонажа..."
          rows={4}
        />
      </Field>

      {errors?.length > 0 && (
        <div className="entity-form__errors">
          {errors.map((msg, i) => <p key={i}>{msg}</p>)}
        </div>
      )}

      <div className="entity-form__actions">
        <button type="button" className="btn-ghost" onClick={onCancel} disabled={loading}>Отмена</button>
        <button type="submit" className="btn-gold" disabled={loading}>
          {loading ? 'Сохранение...' : label}
        </button>
      </div>
    </form>
  );
}

// ── Boss Form ──────────────────────────────────────────────────

function BossForm({ initial = {}, onSubmit, onCancel, loading, errors, label }) {
  const [form, setForm] = useState({
    name:         initial.name         ?? '',
    description:  initial.description  ?? '',
    damageType:   initial.damageType   ?? '',
    bossType:     initial.bossType     ?? '',
    category:     initial.category     ?? '',
    region:       initial.region       ?? '',
    location:     initial.location     ?? '',
    hasWeakPoint: initial.hasWeakPoint ?? false,
    photo:        null,
  });
  const [preview, setPreview] = useState(initial.photoUrl ?? null);

  const set = (field, value) => setForm(f => ({ ...f, [field]: value }));

  function handleImage(e) {
    const file = e.target.files[0];
    if (!file) return;
    set('photo', file);
    setPreview(URL.createObjectURL(file));
  }

  function handleSubmit(e) {
    e.preventDefault();
    const fd = new FormData();
    Object.entries(form).forEach(([k, v]) => {
      if (k === 'photo') { if (v) fd.append('photo', v); }
      else fd.append(k, String(v));
    });
    onSubmit(fd);
  }

  return (
    <form className="entity-form" onSubmit={handleSubmit}>
      <ImageUpload preview={preview} onChange={handleImage} />

      <Field label="Имя">
        <input
          className="entity-form__input"
          value={form.name}
          onChange={e => set('name', e.target.value)}
          placeholder="Имя босса..."
          required
        />
      </Field>

      <Field label="Тип урона">
        <ChipGroup
          options={ELEMENTS.map(e => ({ ...e, color: e.value.toLowerCase() }))}
          value={form.damageType}
          onChange={v => set('damageType', v)}
        />
      </Field>

      <Field label="Тип босса">
        <ChipGroup options={BOSS_TYPES} value={form.bossType} onChange={v => set('bossType', v)} />
      </Field>

      <Field label="Категория">
        <ChipGroup options={BOSS_CATEGORIES} value={form.category} onChange={v => set('category', v)} />
      </Field>

      <Field label="Регион">
        <ChipGroup options={REGIONS} value={form.region} onChange={v => set('region', v)} />
      </Field>

      <Field label="Локация">
        <input
          className="entity-form__input"
          value={form.location}
          onChange={e => set('location', e.target.value)}
          placeholder="Например: Драконий хребет..."
        />
      </Field>

      <Field label="Слабая точка">
        <Toggle
          value={form.hasWeakPoint}
          onChange={v => set('hasWeakPoint', v)}
          labelOn="Есть слабая точка"
          labelOff="Нет слабой точки"
        />
      </Field>

      <Field label="Описание">
        <textarea
          className="entity-form__input entity-form__textarea"
          value={form.description}
          onChange={e => set('description', e.target.value)}
          placeholder="Описание босса..."
          rows={4}
        />
      </Field>

      {errors?.length > 0 && (
        <div className="entity-form__errors">
          {errors.map((msg, i) => <p key={i}>{msg}</p>)}
        </div>
      )}

      <div className="entity-form__actions">
        <button type="button" className="btn-ghost" onClick={onCancel} disabled={loading}>Отмена</button>
        <button type="submit" className="btn-gold" disabled={loading}>
          {loading ? 'Сохранение...' : label}
        </button>
      </div>
    </form>
  );
}

// ── Hero Detail ────────────────────────────────────────────────

function HeroDetail({ entity, onEdit, onDelete, deleting }) {
  const rarityLabel = entity.rarity === 'FiveStar' ? '5★' : '4★';

  return (
    <div className="entity-detail">
      <div className="entity-detail__img-wrap">
        {entity.photoUrl
          ? <img src={entity.photoUrl} alt={entity.name} />
          : <div className="entity-detail__img-placeholder">{entity.name?.[0]}</div>
        }
        <div className="entity-detail__img-gradient" />
        <p className="entity-detail__img-name">{entity.name}</p>
      </div>

      <div className="entity-detail__body">
        <div className="entity-detail__badges">
          {entity.element  && <span className={`badge badge--el-${entity.element?.toLowerCase()}`}>{entity.element}</span>}
          {entity.rarity   && <span className="badge badge--rarity">{rarityLabel}</span>}
          {entity.role     && <span className="badge">{entity.role}</span>}
        </div>

        <div className="detail-grid">
          <DetailRow label="Оружие"          value={entity.weapon} />
          <DetailRow label="Модель"           value={entity.model} />
          <DetailRow label="Командный бонус"  value={entity.teamBonus} />
        </div>

        {entity.description && (
          <p className="entity-detail__desc">{entity.description}</p>
        )}

        <div className="entity-detail__actions">
          <button className="btn-gold" onClick={onEdit}>✎ Редактировать</button>
          <button className="btn-danger" onClick={onDelete} disabled={deleting}>
            {deleting ? 'Удаление...' : '✕ Удалить'}
          </button>
        </div>
      </div>
    </div>
  );
}

// ── Boss Detail ────────────────────────────────────────────────

function BossDetail({ entity, onEdit, onDelete, deleting }) {
  return (
    <div className="entity-detail">
      <div className="entity-detail__img-wrap">
        {entity.photoUrl
          ? <img src={entity.photoUrl} alt={entity.name} />
          : <div className="entity-detail__img-placeholder">{entity.name?.[0]}</div>
        }
        <div className="entity-detail__img-gradient" />
        <p className="entity-detail__img-name">{entity.name}</p>
      </div>

      <div className="entity-detail__body">
        <div className="entity-detail__badges">
          {entity.damageType && <span className={`badge badge--el-${entity.damageType?.toLowerCase()}`}>{entity.damageType}</span>}
          {entity.bossType   && <span className="badge">{entity.bossType}</span>}
          {entity.category   && <span className="badge">{entity.category}</span>}
        </div>

        <div className={`weakpoint-banner ${entity.hasWeakPoint ? 'weakpoint-banner--on' : 'weakpoint-banner--off'}`}>
          {entity.hasWeakPoint ? '⚠ Есть слабая точка' : '— Слабой точки нет'}
        </div>

        <div className="detail-grid">
          <DetailRow label="Регион"  value={entity.region} />
          <DetailRow label="Локация" value={entity.location} />
        </div>

        {entity.description && (
          <p className="entity-detail__desc">{entity.description}</p>
        )}

        <div className="entity-detail__actions">
          <button className="btn-gold" onClick={onEdit}>✎ Редактировать</button>
          <button className="btn-danger" onClick={onDelete} disabled={deleting}>
            {deleting ? 'Удаление...' : '✕ Удалить'}
          </button>
        </div>
      </div>
    </div>
  );
}

// ── Main panel ─────────────────────────────────────────────────

export default function EntityPanel({
  open,
  mode,         // 'detail' | 'create' | 'edit'
  entityType,   // 'hero' | 'boss'
  entity,
  onClose,
  onCreate,
  onUpdate,
  onDelete,
  onSwitchEdit,
  loading,
  deleting,
  errors,
}) {
  useEffect(() => {
    document.body.style.overflow = open ? 'hidden' : '';
    return () => { document.body.style.overflow = ''; };
  }, [open]);

  const typeLabel = entityType === 'hero' ? 'Героя' : 'Босса';

  const titles = {
    detail: entity?.name ?? '',
    create: `Добавить ${typeLabel}`,
    edit:   'Редактировать',
  };

  const FormComponent   = entityType === 'hero' ? HeroForm   : BossForm;
  const DetailComponent = entityType === 'hero' ? HeroDetail : BossDetail;

  return (
    <>
      <div className={`panel-backdrop ${open ? 'open' : ''}`} onClick={onClose} />

      <aside className={`entity-panel ${open ? 'open' : ''}`}>
        <div className="entity-panel__head">
          <p className="entity-panel__title">{titles[mode]}</p>
          <button className="entity-panel__close" onClick={onClose} aria-label="Закрыть">✕</button>
        </div>

        <div className="entity-panel__body">
          {mode === 'detail' && entity && (
            <DetailComponent
              entity={entity}
              onEdit={onSwitchEdit}
              onDelete={onDelete}
              deleting={deleting}
            />
          )}
          {mode === 'create' && (
            <FormComponent
              onSubmit={onCreate}
              onCancel={onClose}
              loading={loading}
              errors={errors}
              label={`Создать ${typeLabel}`}
            />
          )}
          {mode === 'edit' && entity && (
            <FormComponent
              initial={entity}
              onSubmit={(fd) => onUpdate(entity.guid, fd)}
              onCancel={onClose}
              loading={loading}
              errors={errors}
              label="Сохранить"
            />
          )}
        </div>
      </aside>
    </>
  );
}