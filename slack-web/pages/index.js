import { useState } from 'react';
import MainContent from './components/main-content';
import Thread from './components/thread';
import Search from './components/search';
import Sidebar from './components/sidebar';

export default function Home() {
  const [thread, setThread] = useState(null)
  const [threadsData, setThreadsData] = useState([]);
  return (
    <div className={`container ${thread ? 'container-open-thread' : ''}`}>
      <div className="search">
        <Search />
      </div>
      <div className="header">heading</div>
      <div className="sidebar">
        <Sidebar />
      </div>
      {/* <div className="resize">
        Resize
      </div> */}
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
        let existingThread = cloneThreads.find(t => t.id === thread.id)
        if (existingThread) {
          existingThread.replies = [...existingThread.replies];
          existingThread.replies.push({ id: new Date().getTime(), text: reply });
        }
        setThread(Object.assign({}, existingThread));
        setThreadsData(cloneThreads)
      }} />}
    </div>
  );
}
