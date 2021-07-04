import React, { useState } from 'react';
import Messages from './Messages';
import Input from './Input';

const MainContent = () => {
  const [textMessage, setTextMessage] = useState('');
  const [messages, setMessages] = useState([]);
  const [threads, setThreads] = useState([]);

  const handleMoreAction = (action) => {
    const newMessages = messages.map((m) => {
      if (m.id == action.id && m.key != 'reply') {
        const existingReaction = m.reactions.find(
          (rec) => rec.key === action.key
        );
        if (existingReaction) {
          existingReaction.times++;
        } else {
          m.reactions.push({ ...action, times: 1 });
        }
      }
      return m;
    });
    setMessages(newMessages);
  };
  return (
    <div className="main-content">
      <Messages messages={messages} handleMoreAction={handleMoreAction} />
      <form
        onSubmit={(e) => {
          e.preventDefault();
          const newList = [
            ...messages,
            {
              id: messages.length + 1,
              text: textMessage,
              timestamp: new Date(),
              createdBy: '',
              reactions: [],
            },
          ];
          setMessages(newList);
          setTextMessage('');
        }}
      >
        <div className="main-content__input-box">
          <Input
            value={textMessage}
            onChange={(e) => {
              setTextMessage(e.target.value);
            }}
          />
        </div>
      </form>
    </div>
  );
};

export default MainContent;
