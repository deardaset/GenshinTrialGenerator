import '../css/EntityCard.css';

const ELEMENT_COLORS = {
  Pyro:     { label: 'Пиро',    color: '#e2603a' },
  Hydro:    { label: 'Гидро',   color: '#4a9eca' },
  Anemo:    { label: 'Анемо',   color: '#74c2a8' },
  Electro:  { label: 'Электро', color: '#9d72c8' },
  Dendro:   { label: 'Дендро',  color: '#7ab848' },
  Cryo:     { label: 'Крио',    color: '#98d4e8' },
  Geo:      { label: 'Гео',     color: '#c8a028' },
};

export default function EntityCard({ entity, onClick }) {
  const el = ELEMENT_COLORS[entity.element] || { label: entity.element, color: 'var(--gold)' };
  const stars = entity.rarity || 4;

  return (
    <div
      className={`entity-card rarity-${stars}`}
      onClick={() => onClick(entity)}
      role="button"
      tabIndex={0}
      onKeyDown={(e) => e.key === 'Enter' && onClick(entity)}
    >
      {/* Rarity bar */}
      <div className="entity-card__rarity-bar" />

      {/* Image */}
      <div className="entity-card__img-wrap">
        {entity.photoUrl
          ? <img src={entity.photoUrl} alt={entity.name} className="entity-card__img" />
          : <div className="entity-card__img-placeholder">
              <span>{entity.name?.[0] ?? '?'}</span>
            </div>
        }
        {/* Element badge */}
        <span
          className="entity-card__element"
          style={{ '--el-color': el.color }}
        >
          {el.label}
        </span>
      </div>

      {/* Footer */}
      <div className="entity-card__footer">
        <p className="entity-card__name">{entity.name}</p>
        <div className="entity-card__stars">
          {'★'.repeat(stars)}{'☆'.repeat(5 - stars)}
        </div>
      </div>
    </div>
  );
}