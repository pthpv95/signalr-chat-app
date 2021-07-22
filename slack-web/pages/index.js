import { useRef, useState } from 'react';
import MainContent from './components/main-content';
import Search from './components/search';
import Sidebar from './components/sidebar';
import Thread from './components/thread';

export default function Home() {
  const [thread, setThread] = useState(null)
  const [threadsData, setThreadsData] = useState([]);
  const sbRef = useRef();
  const resizeRef = useRef();
  const [sidebarWidth, setSidebarWidth] = useState(0);

  const handleSubmitReply = (reply, thread) => {
    const cloneThreads = Object.assign([], threadsData);
    let existingThread = cloneThreads.find(t => t.id === thread.id)
    if (existingThread) {
      existingThread.replies = [...existingThread.replies];
      existingThread.replies.push({ id: new Date().getTime(), text: reply });
    }
    setThread(Object.assign({}, existingThread));
    setThreadsData(cloneThreads)
  }

  return (
    <div className={`container ${thread ? 'container-open-thread' : ''}`}>
      <div className="search">
        <Search />
      </div>
      <div className="header">heading</div>
      <div className="sidebar" ref={sbRef} style={{ width: sidebarWidth ? sidebarWidth : 'auto' }}>
        <Sidebar />
      </div>
      <div draggable className="resize" ref={resizeRef}
        onDrag={(e) => {
          e.preventDefault();
          if (e.pageX === 0 || e.pageX < 280) {
            return;
          }
          setSidebarWidth(e.pageX);
        }}
        onDragEnd={(e) => { }}
        onDragStart={(e) => {
          const img = new Image();
          img.src = 'data:image/gif;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNkYAAAAAYAAjCB0C8AAAAASUVORK5CYII=';
          e.dataTransfer.setDragImage(img, 0, 0);
        }}>
      </div>
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
      {thread !== null && <Thread thread={thread} onCloseThread={() => setThread(null)} onSubmit={handleSubmitReply} />}
    </div>
  );
}
