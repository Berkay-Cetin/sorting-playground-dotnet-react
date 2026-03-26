const SPEEDS = [
  { label: 'Slow', value: 400 },
  { label: 'Normal', value: 120 },
  { label: 'Fast', value: 40 },
  { label: 'Turbo', value: 8 },
];

export function SpeedControl({ value, onChange, disabled }) {
  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
      <label style={{
        fontFamily: 'var(--font-mono)',
        fontSize: '11px',
        letterSpacing: '0.1em',
        color: 'var(--text-muted)',
      }}>
        SPEED
      </label>
      <div style={{ display: 'flex', gap: '6px' }}>
        {SPEEDS.map(speed => {
          const active = value === speed.value;
          return (
            <button
              key={speed.label}
              onClick={() => onChange(speed.value)}
              disabled={disabled}
              style={{
                padding: '7px 14px',
                borderRadius: '6px',
                border: active
                  ? '1.5px solid var(--accent2)'
                  : '1.5px solid var(--border)',
                background: active
                  ? 'rgba(6,182,212,0.12)'
                  : 'var(--surface2)',
                color: active ? 'var(--accent2)' : 'var(--text-muted)',
                fontFamily: 'var(--font-mono)',
                fontSize: '12px',
                cursor: disabled ? 'not-allowed' : 'pointer',
                opacity: disabled ? 0.5 : 1,
                transition: 'all 0.2s',
              }}
            >
              {speed.label}
            </button>
          );
        })}
      </div>
    </div>
  );
}