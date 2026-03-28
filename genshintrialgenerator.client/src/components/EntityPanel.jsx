import { useState, useEffect } from 'react';
import '../css/EntityPanel.css';

const ELEMENTS = ['Pyro', 'Hydro', 'Anemo', 'Electro', 'Dendro', 'Cryo', 'Geo'];
const ELEMENT_LABELS = {
  Pyro: 'Пиро', Hydro: 'Гидро', Anemo: 'Анемо',
  Electro: 'Электро', Dendro: 'Дендро', Cryo: 'Крио', Geo: 'Гео',
};

// ── Shared form used for both Create and Edit ──────────────────
function EntityForm({ initial = {}, onSubmit, onCancel, loading, errors, label }) {
  const [form, setForm] = useState({
    name:        initial.name        ?? '',
    description: initial.description ?? '',
    element:     initial.element     ?? '',
    rarity:      initial.rarity      ?? 4,
    image:       null,
  });
  const [preview, setPreview] = useState(initial.imageUrl ?? null);

  function set(field, value) {
    setForm(f => ({ ...f, [field]: value }));
  }

  function handleImage(e) {
    const file = e.target.files[0];
    if (!file) return;
    set('image', file);
    setPreview(URL.createObjectURL(file));
  }

  function handleSubmit(e) {
    e.preventDefault();
    const fd = new FormData();
    fd.append('name', form.name);
    fd.append('description', form.description);
    fd.append('element', form.element);
    fd.append('rarity', form.rarity);
    if (form.image) fd.append('image', form.image);
    onSubmit(fd);
  }

  return (
    <form className="entity-form" onSubmit={handleSubmit}>
      {/* Image upload */}
      <label className="entity-form__img-upload">
        <input type="file" accept="image/*" onChange={handleImage} hidden />
        {preview
          ? <img src={preview} alt="preview" className="entity-form__img-preview" />
          : <div className="entity-form__img-placeholder">
              <span>+</span>
              <p>Загрузить изображение</p>
            </div>
        }
        <div className="entity-form__img-overlay">Изменить</div>
      </label>

      {/* Name */}
      <div className="entity-form__field">
        <label className="entity-form__label">Имя</label>
        <input
          className="entity-form__input"
          value={form.name}
          onChange={e => set('name', e.target.value)}
          placeholder="Название персонажа..."
          required
        />
      </div>

      {/* Element */}
      <div className="entity-form__field">
        <label className="entity-form__label">Элемент</label>
        <div className="entity-form__elements">
          {ELEMENTS.map(el => (
            <button
              key={el}
              type="button"
              className={`entity-form__el-btn el-${el.toLowerCase()} ${form.element === el ? 'active' : ''}`}
              onClick={() => set('element', el)}
            >
              {ELEMENT_LABELS[el]}
            </button>
          ))}
        </div>
      </div>

      {/* Rarity */}
      <div className="entity-form__field">
        <label className="entity-form__label">Редкость</label>
        <div className="entity-form__stars">
          {[1, 2, 3, 4, 5].map(n => (
            <button
              key={n}
              type="button"
              className={`entity-form__star ${n <= form.rarity ? 'active' : ''}`}
              onClick={() => set('rarity', n)}
            >★</button>
          ))}
        </div>
      </div>

      {/* Description */}
      <div className="entity-form__field">
        <label className="entity-form__label">Описание</label>
        <textarea
          className="entity-form__input entity-form__textarea"
          value={form.description}
          onChange={e => set('description', e.target.value)}
          placeholder="Описание..."
          rows={4}
        />
      </div>

      {/* Errors */}
      {errors?.length > 0 && (
        <div className="entity-form__errors">
          {errors.map((msg, i) => <p key={i}>{msg}</p>)}
        </div>
      )}

      {/* Actions */}
      <div className="entity-form__actions">
        <button type="button" className="btn-ghost" onClick={onCancel} disabled={loading}>
          Отмена
        </button>
        <button type="submit" className="btn-gold" disabled={loading}>
          {loading ? 'Сохранение...' : label}
        </button>
      </div>
    </form>
  );
}

// ── Detail view with Edit / Delete buttons ─────────────────────
function EntityDetail({ entity, onEdit, onDelete, onClose, deleting }) {
  const stars = entity.rarity || 4;

  return (
    <div className="entity-detail">
      <div className="entity-detail__img-wrap">
        {entity.imageUrl
          ? <img src={entity.imageUrl} alt={entity.name} />
          : <div className="entity-detail__img-placeholder">{entity.name?.[0]}</div>
        }
        <div className="entity-detail__img-gradient" />
        <p className="entity-detail__img-name">{entity.name}</p>
      </div>

      <div className="entity-detail__body">
        <div className="entity-detail__meta">
          {entity.element && (
            <span className="entity-detail__tag">{entity.element}</span>
          )}
          <span className="entity-detail__stars">
            {'★'.repeat(stars)}{'☆'.repeat(5 - stars)}
          </span>
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
  mode,          // 'detail' | 'create' | 'edit'
  entity,
  onClose,
  onCreate,
  onUpdate,
  onDelete,
  onSwitchEdit,
  loading,
  deleting,
  errors,
  label,         // 'Герой' | 'Босс'
}) {
  // lock body scroll when open
  useEffect(() => {
    document.body.style.overflow = open ? 'hidden' : '';
    return () => { document.body.style.overflow = ''; };
  }, [open]);

  const titles = {
    detail: entity?.name ?? '',
    create: `Добавить ${label}`,
    edit:   `Редактировать`,
  };

  return (
    <>
      {/* Backdrop */}
      <div
        className={`panel-backdrop ${open ? 'open' : ''}`}
        onClick={onClose}
      />

      {/* Panel */}
      <aside className={`entity-panel ${open ? 'open' : ''}`}>
        {/* Panel header */}
        <div className="entity-panel__head">
          <p className="entity-panel__title">{titles[mode]}</p>
          <button className="entity-panel__close" onClick={onClose} aria-label="Закрыть">✕</button>
        </div>

        {/* Content */}
        <div className="entity-panel__body">
          {mode === 'detail' && entity && (
            <EntityDetail
              entity={entity}
              onEdit={onSwitchEdit}
              onDelete={onDelete}
              onClose={onClose}
              deleting={deleting}
            />
          )}
          {mode === 'create' && (
            <EntityForm
              onSubmit={onCreate}
              onCancel={onClose}
              loading={loading}
              errors={errors}
              label={`Создать ${label}`}
            />
          )}
          {mode === 'edit' && entity && (
            <EntityForm
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