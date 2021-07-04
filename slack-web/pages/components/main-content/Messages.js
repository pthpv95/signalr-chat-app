import React, { useState } from 'react';

const MoreAction = ({ message, handleMoreAction }) => {
  const actions = [
    {
      key: 'like',
      text: '👍',
    },
    {
      key: 'haha',
      text: '😂',
    },
    {
      key: 'reply',
      text: 'Reply',
    },
  ];
  return (
    <div className="message__more-actions">
      {actions.map((action) => {
        return (
          <div
            key={action.key}
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

const MessageItem = ({ message, handleMoreAction }) => {
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
      <img
        className="message__img"
        src="https://ca.slack-edge.com/TEGSLQ86L-U01JM86M3HN-1e6b3d3e7eaf-512"
      />
      <div className="message__content">
        <p className="message__content--username">
          Lee Pham
          <span>{new Date(message.timestamp).toLocaleTimeString()}</span>
        </p>
        <p>{message.text}</p>
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
      </div>
      {isHover && (
        <MoreAction message={message} handleMoreAction={handleMoreAction} />
      )}
    </div>
  );
};

const Messages = ({ messages, handleMoreAction }) => {
  return (
    <div className="message-list">
      {messages.map((item, index) => {
        return (
          <div key={`message_${index}`}>
            <MessageItem
              key={index}
              message={item}
              handleMoreAction={handleMoreAction}
            />
            {index !== messages.length - 1 && <div className="line-break" />}
          </div>
        );
      })}
    </div>
  );
};

export default React.memo(Messages);
