import { useState, useEffect, useRef } from 'react';

export function CommandPalette({ algorithms, selected, onSelect, disabled }) {
  const [open, setOpen] = useState(false);
  const [query, setQuery] = useState('');
  const inputRef = useRef(null);
  const paletteRef = useRef(null);

  const filtered = algorithms.filter(a =>
    a.toLowerCase().includes(query.toLowerCase())
  );

  useEffect(() => {
    if (open) setTimeout(() => inputRef.current?.focus(), 50);
  }, [open]);

  useEffect(() => {
    const handler = (e) => {
      if (e.key === 'Escape') setOpen(false);
      if ((e.metaKey || e.ctrlKey) && e.key === 'k') {
        e.preventDefault();
        if (!disabled) setOpen(o => !o);
      }
    };
    window.addEventListener('keydown', handler);
    return () => window.removeEventListener('keydown', handler);
  }, [disabled]);

  useEffect(() => {
    const handler = (e) => {
      if (paletteRef.current && !paletteRef.current.contains(e.target)) {
        setOpen(false);
      }
    };
    if (open) document.addEventListener('mousedown', handler);
    return () => document.removeEventListener('mousedown', handler);
  }, [open]);

  const handleSelect = (algo) => {
    onSelect(algo);
    setOpen(false);
    setQuery('');
  };

  return (
    <>
      {/* Trigger button */}
      <button
        onClick={() => !disabled && setOpen(true)}
        disabled={disabled}
        style={{
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          width: '100%',
          maxWidth: '420px',
          padding: '12px 16px',
          background: 'var(--surface2)',
          border: '1px solid var(--border)',
          borderRadius: '10px',
          color: selected ? 'var(--text)' : 'var(--text-muted)',
          fontFamily: 'var(--font-mono)',
          fontSize: '14px',
          cursor: disabled ? 'not-allowed' : 'pointer',
          opacity: disabled ? 0.5 : 1,
          transition: 'border-color 0.2s, box-shadow 0.2s',
          outline: 'none',
        }}
        onMouseEnter={e => {
          if (!disabled) e.currentTarget.style.borderColor = 'var(--accent2)';
        }}
        onMouseLeave={e => {
          e.currentTarget.style.borderColor = 'var(--border)';
        }}
      >
        <span style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
          <span style={{ color: 'var(--accent2)', fontSize: '12px' }}>⌘</span>
          {selected ?? 'Select algorithm...'}
        </span>
        <span style={{
          fontSize: '11px',
          color: 'var(--text-muted)',
          background: 'var(--surface)',
          border: '1px solid var(--border)',
          borderRadius: '4px',
          padding: '2px 7px',
          letterSpacing: '0.05em',
        }}>
          ⌘K
        </span>
      </button>

      {/* Overlay */}
      {open && (
        <div style={{
          position: 'fixed',
          inset: 0,
          background: 'rgba(0,0,0,0.7)',
          backdropFilter: 'blur(4px)',
          zIndex: 100,
          display: 'flex',
          alignItems: 'flex-start',
          justifyContent: 'center',
          paddingTop: '18vh',
          animation: 'fadeIn 0.15s ease',
        }}>
          <div
            ref={paletteRef}
            style={{
              width: '100%',
              maxWidth: '480px',
              background: 'var(--surface)',
              border: '1px solid var(--border)',
              borderRadius: '16px',
              boxShadow: '0 24px 80px rgba(0,0,0,0.6), 0 0 0 1px rgba(124,58,237,0.15)',
              overflow: 'hidden',
              animation: 'slideDown 0.18s cubic-bezier(0.4,0,0.2,1)',
            }}
          >
            {/* Search input */}
            <div style={{
              display: 'flex',
              alignItems: 'center',
              gap: '12px',
              padding: '16px 20px',
              borderBottom: '1px solid var(--border)',
            }}>
              <span style={{ color: 'var(--text-muted)', fontSize: '16px' }}>⌕</span>
              <input
                ref={inputRef}
                value={query}
                onChange={e => setQuery(e.target.value)}
                placeholder="Search algorithm..."
                style={{
                  flex: 1,
                  background: 'none',
                  border: 'none',
                  outline: 'none',
                  color: 'var(--text)',
                  fontFamily: 'var(--font-mono)',
                  fontSize: '15px',
                  caretColor: 'var(--accent2)',
                }}
              />
              <span style={{
                fontSize: '11px',
                color: 'var(--text-muted)',
                background: 'var(--surface2)',
                border: '1px solid var(--border)',
                borderRadius: '4px',
                padding: '2px 7px',
              }}>
                ESC
              </span>
            </div>

            {/* Results */}
            <div style={{
              maxHeight: '320px',
              overflowY: 'auto',
              padding: '8px',
            }}>
              {filtered.length === 0 ? (
                <div style={{
                  padding: '24px',
                  textAlign: 'center',
                  color: 'var(--text-muted)',
                  fontFamily: 'var(--font-mono)',
                  fontSize: '13px',
                }}>
                  No algorithms found
                </div>
              ) : (
                filtered.map((algo, i) => {
                  const isSelected = algo === selected;
                  return (
                    <button
                      key={algo}
                      onClick={() => handleSelect(algo)}
                      style={{
                        width: '100%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between',
                        padding: '11px 14px',
                        borderRadius: '8px',
                        border: 'none',
                        background: isSelected ? 'rgba(6,182,212,0.1)' : 'transparent',
                        color: isSelected ? 'var(--accent2)' : 'var(--text)',
                        fontFamily: 'var(--font-mono)',
                        fontSize: '14px',
                        cursor: 'pointer',
                        textAlign: 'left',
                        transition: 'background 0.15s',
                        animation: `fadeIn 0.15s ease ${i * 0.03}s both`,
                      }}
                      onMouseEnter={e => {
                        if (!isSelected) e.currentTarget.style.background = 'var(--surface2)';
                      }}
                      onMouseLeave={e => {
                        if (!isSelected) e.currentTarget.style.background = 'transparent';
                      }}
                    >
                      <span style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                        <span style={{
                          width: '6px', height: '6px',
                          borderRadius: '50%',
                          background: isSelected ? 'var(--accent2)' : 'var(--border)',
                          flexShrink: 0,
                          transition: 'background 0.15s',
                        }} />
                        {algo}
                      </span>
                      {isSelected && (
                        <span style={{ fontSize: '12px', color: 'var(--accent2)' }}>✓</span>
                      )}
                    </button>
                  );
                })
              )}
            </div>

            {/* Footer */}
            <div style={{
              padding: '10px 20px',
              borderTop: '1px solid var(--border)',
              display: 'flex',
              gap: '16px',
              alignItems: 'center',
            }}>
              <span style={{ color: 'var(--text-muted)', fontFamily: 'var(--font-mono)', fontSize: '11px' }}>
                {filtered.length} algorithm{filtered.length !== 1 ? 's' : ''}
              </span>
              <span style={{ color: 'var(--text-muted)', fontFamily: 'var(--font-mono)', fontSize: '11px', marginLeft: 'auto' }}>
                ↵ to select
              </span>
            </div>
          </div>
        </div>
      )}

      <style>{`
        @keyframes fadeIn { from { opacity: 0 } to { opacity: 1 } }
        @keyframes slideDown { from { opacity: 0; transform: translateY(-12px) scale(0.98) } to { opacity: 1; transform: translateY(0) scale(1) } }
      `}</style>
    </>
  );
}