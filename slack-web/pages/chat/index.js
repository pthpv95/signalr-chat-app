import NextImage from 'next/image';
import { useRouter } from 'next/router';
import { useEffect, useRef, useState } from 'react';
import MainContent from './components/main-content';
import Search from './components/search';
import Sidebar from './components/sidebar';
import Thread from './components/thread';

export default function Chat() {
  const [thread, setThread] = useState(null)
  const [threadsData, setThreadsData] = useState([]);
  const [sidebarWidth, setSidebarWidth] = useState(0);
  const [selectedDirectMessage, setSelectedDirectMessage] = useState(null);
  const [initialSidebarWidth, setInitialSizeBarWidth] = useState(0)
  const router = useRouter()

  const sidebarRef = useRef();
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

  const handleDrag = (e) => {
    e.preventDefault();
    if (e.pageX === 0 || e.pageX < initialSidebarWidth) {
      e.stopPropagation();
      e.preventDefault();
      e.cancelable = true;
      return;
    }
    setSidebarWidth(e.pageX);
  }

  // useEffect(() => {
  //   if(!(user || isLoading)){
  //     // router.push('/login')
  //   }
  // }, [user, isLoading, router])

  useEffect(() => {
    if(sidebarRef.current){
      setInitialSizeBarWidth(sidebarRef.current.clientWidth);
    }
  }, [])

  return (
    <>
      <div className={`container ${thread ? 'container-open-thread' : ''}`}>
        <div className="search">
          <Search />
        </div>
        <div className="header">
          {selectedDirectMessage &&
            <div className="header__selected-contact">
              <NextImage src={`/assets/${selectedDirectMessage.avatar}`}
                alt={selectedDirectMessage.name}
                width={30}
                height={30}
              />
              <p>{selectedDirectMessage.name}</p>
            </div>
          }
        </div>
        <div className="sidebar" style={{ width: sidebarWidth ? sidebarWidth : 'auto' }} ref={sidebarRef}>
          <Sidebar onDirectMessageClick={(data) => {
            setSelectedDirectMessage(data)
          }} />
        </div>
        <div draggable className="resize"
          onDrag={handleDrag}
          onDragEnd={(e) => { }}
          onDragStart={(e) => {
            const img = new Image();
            img.src = 'data:image/gif;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNkYAAAAAYAAjCB0C8AAAAASUVORK5CYII=';
            e.dataTransfer.setDragImage(img, 0, 0);
          }}>
        </div>
        <div className={`main-chat ${thread ? 'main-open-thread' : ''}`}>
          <MainContent
            onSendMessage={(value) => {
              
            }}
            onOpenThread={(thread) => {
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
    </>
  );
}
