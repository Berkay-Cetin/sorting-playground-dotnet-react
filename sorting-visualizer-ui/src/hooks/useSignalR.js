import { useEffect, useRef, useState } from 'react';
import * as signalR from '@microsoft/signalr';

const HUB_URL = 'http://localhost:5152/hubs/sorting';

export function useSignalR(onStep) {
  const connectionRef = useRef(null);
  const [connectionId, setConnectionId] = useState(null);
  const [connected, setConnected] = useState(false);
  const onStepRef = useRef(onStep);
  onStepRef.current = onStep;

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(HUB_URL)
      .withAutomaticReconnect()
      .build();

    connection.on('SortStep', (data) => {
      onStepRef.current(data);
    });

    connection.start().then(() => {
      setConnectionId(connection.connectionId);
      setConnected(true);
    });

    connectionRef.current = connection;

    return () => { connection.stop(); };
  }, []);

  return { connectionId, connected };
}