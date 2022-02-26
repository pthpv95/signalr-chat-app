import React, { useEffect, useRef, useState } from 'react';
import Avatar from './avatar';

const MoreAction = ({ message, handleMoreAction }) => {
  const actions = [
    {
      type: 'like',
      text: '👍',
    },
    {
      type: 'haha',
      text: '😂',
    },
    {
      type: 'reply',
      text: 'Reply',
    },
  ];
  return (
    <div className="message__more-actions">
      {actions.map((action) => {
        return (
          <div
            key={action.type}
            className="message__more-actions--item"
            onClick={(e) => {
              handleMoreAction({ ...action, id: message.id });
            }}
          >
            {action.text}
          </div>
        );
      })}
    </div>
  );
};

const MessageItem = ({ message, isInThread, handleMoreAction }) => {
  const [isHover, setIsHover] = useState(false);

  return (
    <div
      className="message"
      onMouseEnter={(e) => {
        setIsHover(true);
      }}
      onMouseLeave={() => {
        setIsHover(false);
      }}
    >
      <Avatar />
      <div className="message__content">
        <p className="message__content--username">
          Lee Pham
          {!isInThread && (
            <span>{new Date(message.timestamp).toLocaleTimeString()}</span>
          )}
        </p>
        <p>{message.text}</p>
        {!isInThread && (
          <div className="message__content--reactions">
            {message.reactions &&
              message.reactions.map((reaction) => {
                return (
                  <div key={`reaction_${reaction.key}`}>
                    {reaction.text} {reaction.times}
                  </div>
                );
              })}
          </div>
        )}
      </div>
      {isHover && !isInThread && (
        <MoreAction message={message} handleMoreAction={handleMoreAction} />
      )}
    </div>
  );
};

const Messages = ({ messages, isInThread, handleMoreAction }) => {
  const messagesRef = useRef();
  useEffect(() => {
    if (messagesRef && messagesRef.current) {
      messagesRef.current.scroll({
        top: messagesRef.current.scrollHeight,
        behavior: 'smooth',
      });
    }
  }, [messages]);
  return (
    <div className="message-list" ref={messagesRef}>
      {messages && messages.map((item, index) => {
        return (
          <div key={`message_${item.id}`}>
            <MessageItem
              key={index}
              message={item}
              isInThread={isInThread}
              handleMoreAction={handleMoreAction}
            />
            {index !== messages.length - 1 && <div className="line-break" />}
          </div>
        );
      })}
    </div>
  );
};

export default React.memo(Messages, (prev, next) => prev.messages === next.messages);
