import { SortBar } from './SortBar';

export function SortVisualizer({ values, highlighted, done }) {
  const max = Math.max(...values, 1);

  return (
    <div style={{
      display: 'flex',
      alignItems: 'flex-end',
      gap: '6px',
      height: '280px',
      padding: '0 8px',
      width: '100%',
    }}>
      {values.map((v, i) => (
        <SortBar
          key={i}
          value={v}
          max={max}
          highlighted={highlighted.includes(i)}
          done={done}
        />
      ))}
    </div>
  );
}