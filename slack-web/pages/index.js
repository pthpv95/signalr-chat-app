import { useState } from 'react';
import MainContent from './components/main-content';
import Thread from './components/thread';

export default function Home() {
  const [thread, setThread] = useState(null)
  const [threadsData, setThreadsData] = useState([]);
  return (
    <div className={`container ${thread ? 'container-open-thread' : ''}`}>
      <div className="search">search</div>
      <div className="heading">heading</div>
      <div className="sidebar">side bar</div>
      <div className={`main-chat ${thread ? 'main-open-thread' : ''}`}>
        <MainContent onOpenThread={(thread) => {
          const existingThread = threadsData.find(t => t.id === thread.id)
          if (!existingThread) {
            setThread(thread);
            setThreadsData([...threadsData, thread])
          } else {
            setThread(thread);
          }
        }} />
      </div>
      {thread && <Thread thread={thread} onCloseThread={() => setThread(null)} onSubmit={(reply, thread) => {
        const cloneThreads = Object.assign([], threadsData);
        const existingThread = cloneThreads.find(t => t.id === thread.id)
        if (existingThread) {
          existingThread.replies.push({ id: new Date().getTime(), text: reply });
        }
        setThread(Object.assign({}, existingThread));
        setThreadsData(cloneThreads)
      }} />}
    </div>
  );
}
