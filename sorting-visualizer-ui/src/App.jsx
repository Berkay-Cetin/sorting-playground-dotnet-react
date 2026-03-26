import { useState, useEffect, useCallback } from 'react';
import { useSignalR } from './hooks/useSignalR';
import { SortVisualizer } from './components/SortVisualizer';
import { CommandPalette } from './components/CommandPalette';
import { StatsPanel } from './components/StatsPanel';
import { SpeedControl } from './components/SpeedControl';

const API = 'http://localhost:5152/api/sorting';

export default function App() {
  const [algorithms, setAlgorithms] = useState([]);
  const [selected, setSelected] = useState(null);
  const [values, setValues] = useState([]);
  const [highlighted, setHighlighted] = useState([]);
  const [steps, setSteps] = useState(0);
  const [status, setStatus] = useState('idle');
  const [delayMs, setDelayMs] = useState(120);

  const handleStep = useCallback((data) => {
    setValues(data.values);
    setHighlighted(data.highlighted ?? []);
    setSteps(s => s + 1);
  }, []);

  const { connectionId, connected } = useSignalR(handleStep);

  useEffect(() => {
    fetch(`${API}/algorithms`)
      .then(r => r.json())
      .then(data => {
        setAlgorithms(data);
        setSelected(data[0] ?? null);
      });

    fetch(`${API}/generate`)
      .then(r => r.json())
      .then(setValues);
  }, []);

  const handleGenerate = async () => {
    const res = await fetch(`${API}/generate`);
    const data = await res.json();
    setValues(data);
    setHighlighted([]);
    setSteps(0);
    setStatus('idle');
  };

  const handleSort = async () => {
    if (!selected || !connectionId || status === 'running') return;
    setSteps(0);
    setHighlighted([]);
    setStatus('running');

    await fetch(`${API}/sort`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        algorithmName: selected,
        connectionId,
        list: values,
        delayMs,
      }),
    });

    setHighlighted([]);
    setStatus('done');
  };

  return (
    <div style={{
      minHeight: '100vh',
      background: 'var(--bg)',
      padding: '40px 48px',
      display: 'flex',
      flexDirection: 'column',
      gap: '32px',
      maxWidth: '960px',
      margin: '0 auto',
    }}>
      {/* Header */}
      <div>
        <h1 style={{
          fontFamily: 'var(--font-main)',
          fontSize: '32px',
          fontWeight: 800,
          letterSpacing: '-0.03em',
          background: 'linear-gradient(90deg, var(--accent2), var(--accent))',
          WebkitBackgroundClip: 'text',
          WebkitTextFillColor: 'transparent',
          marginBottom: '4px',
        }}>
          Sorting Playground
        </h1>
        <p style={{ color: 'var(--text-muted)', fontFamily: 'var(--font-mono)', fontSize: '12px' }}>
          {connected ? '● connected' : '○ connecting...'}
        </p>
      </div>

      {/* Visualizer Card */}
      <div style={{
        background: 'var(--surface)',
        border: '1px solid var(--border)',
        borderRadius: '16px',
        padding: '28px 24px 20px',
        display: 'flex',
        flexDirection: 'column',
        gap: '20px',
      }}>
        <StatsPanel steps={steps} listSize={values.length} status={status} />
        <SortVisualizer values={values} highlighted={highlighted} done={status === 'done'} />
      </div>

      {/* Controls */}
      <div style={{
        background: 'var(--surface)',
        border: '1px solid var(--border)',
        borderRadius: '16px',
        padding: '24px',
        display: 'flex',
        flexDirection: 'column',
        gap: '20px',
      }}>
        <div>
          <label style={{
            display: 'block',
            fontFamily: 'var(--font-mono)',
            fontSize: '11px',
            letterSpacing: '0.1em',
            color: 'var(--text-muted)',
            marginBottom: '10px',
          }}>
            ALGORITHM
          </label>
          <CommandPalette
            algorithms={algorithms}
            selected={selected}
            onSelect={setSelected}
            disabled={status === 'running'}
          />
        </div>

        <SpeedControl
          value={delayMs}
          onChange={setDelayMs}
          disabled={status === 'running'}
        />

        <div style={{ display: 'flex', gap: '12px' }}>
          <button
            onClick={handleGenerate}
            disabled={status === 'running'}
            style={btnStyle('secondary', status === 'running')}
          >
            ↺ New List
          </button>
          <button
            onClick={handleSort}
            disabled={!connected || status === 'running' || values.length === 0}
            style={btnStyle('primary', !connected || status === 'running')}
          >
            {status === 'running' ? '◌ Sorting...' : '▶ Sort'}
          </button>
        </div>
      </div>

      {/* Footer hint */}
      <p style={{
        color: 'var(--text-muted)',
        fontFamily: 'var(--font-mono)',
        fontSize: '11px',
        textAlign: 'center',
        opacity: 0.5,
      }}>
        Add new algorithms to <code>Algorithms/</code> — they appear here automatically
      </p>
    </div>
  );
}

function btnStyle(variant, disabled) {
  const base = {
    padding: '10px 24px',
    borderRadius: '8px',
    fontFamily: 'var(--font-mono)',
    fontSize: '13px',
    fontWeight: 700,
    cursor: disabled ? 'not-allowed' : 'pointer',
    transition: 'all 0.2s',
    opacity: disabled ? 0.5 : 1,
    border: 'none',
  };
  if (variant === 'primary') return {
    ...base,
    background: 'linear-gradient(135deg, var(--accent), var(--accent2))',
    color: '#fff',
  };
  return {
    ...base,
    background: 'var(--surface2)',
    color: 'var(--text-muted)',
    border: '1px solid var(--border)',
  };
}