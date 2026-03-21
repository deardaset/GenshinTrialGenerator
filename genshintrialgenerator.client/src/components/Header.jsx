import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";

const StarIcon = () => (
  <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
    <path d="M8 0L9.8 5.8L16 8L9.8 10.2L8 16L6.2 10.2L0 8L6.2 5.8L8 0Z" fill="currentColor"/>
  </svg>
);

const WindIcon = () => (
  <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
    <path d="M12 2C8 2 4 5 4 9C4 11 5 13 7 14H17C19 13 20 11 20 9C20 5 16 2 12 2Z" stroke="currentColor" strokeWidth="1.5" fill="none"/>
    <path d="M7 14L5 20M17 14L19 20M9 20H15" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round"/>
  </svg>
);

const navItems = [
  { path: "/", label: "Главная", en: "Home", icon: "✦" },
  { path: "/heroes", label: "Герои", en: "Heroes", icon: "⚔" },
  { path: "/bosses", label: "Боссы", en: "Bosses", icon: "☽" },
];

export default function Header() {
  const location = useLocation();
  const [scrolled, setScrolled] = useState(false);
  const [menuOpen, setMenuOpen] = useState(false);

  useEffect(() => {
    const onScroll = () => setScrolled(window.scrollY > 30);
    window.addEventListener("scroll", onScroll);
    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  return (
    <header className={`genshin-header ${scrolled ? "scrolled" : ""}`}>
      <div className="header-glow-top" />

      {/* Floating particles */}
      <div className="particles" aria-hidden="true">
        {[...Array(8)].map((_, i) => (
          <span key={i} className={`particle particle-${i + 1}`} />
        ))}
      </div>

      <div className="header-inner">
        {/* Logo */}
        <Link to="/" className="logo" aria-label="GenshinTrialGenerator Home">
          <div className="logo-emblem">
            <div className="emblem-ring" />
            <div className="emblem-ring emblem-ring-2" />
            <WindIcon />
          </div>
          <div className="logo-text">
            <span className="logo-title">Genshin</span>
            <span className="logo-subtitle">Trial Generator</span>
          </div>
        </Link>

        {/* Desktop nav */}
        <nav className="nav-desktop" aria-label="Main navigation">
          {navItems.map(({ path, label, en, icon }) => (
            <Link
              key={path}
              to={path}
              className={`nav-link ${location.pathname === path ? "active" : ""}`}
            >
              <span className="nav-icon">{icon}</span>
              <span className="nav-label">{label}</span>
              <span className="nav-en">{en}</span>
              <span className="nav-underline" />
            </Link>
          ))}
        </nav>

        {/* Right badge */}
        <div className="header-badge">
          <StarIcon />
          <span>v2.4</span>
        </div>

        {/* Mobile toggle */}
        <button
          className={`menu-toggle ${menuOpen ? "open" : ""}`}
          onClick={() => setMenuOpen((v) => !v)}
          aria-label="Toggle menu"
        >
          <span /><span /><span />
        </button>
      </div>

      {/* Mobile nav */}
      <nav className={`nav-mobile ${menuOpen ? "open" : ""}`} aria-label="Mobile navigation">
        {navItems.map(({ path, label, en, icon }) => (
          <Link
            key={path}
            to={path}
            className={`nav-mobile-link ${location.pathname === path ? "active" : ""}`}
            onClick={() => setMenuOpen(false)}
          >
            <span className="nav-icon">{icon}</span>
            <span>{label}</span>
            <span className="nav-mobile-en">{en}</span>
          </Link>
        ))}
      </nav>

      <div className="header-border-bottom" />
    </header>
  );
}
