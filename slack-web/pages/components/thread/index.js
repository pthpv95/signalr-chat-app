import React, { useEffect, useState } from 'react';
import Input from '../main-content/Input';
import Messages from '../main-content/Messages';

const Thread = ({ thread, onSubmit, onMoreAction, onCloseThread }) => {
  const [reply, setReply] = useState('');
  return (
    <div className="thread">
      <div className="thread__heading">
        <h4>Thread</h4>
        <button onClick={onCloseThread}>X</button>
      </div>
      <div className="">
        <h4>{thread.title}</h4>
        <h4>{thread.replies.length} replies</h4>
      </div>
      <div className="thread">
        <div className="thread__content">
          <Messages
            messages={thread.replies}
            isInThread={true}
            handleMoreAction={onMoreAction}
          />
        </div>
      </div>
      <form onSubmit={(e) => {
        e.preventDefault();
        onSubmit(reply, thread);
        setReply('')
      }}>
        <div className="thread__input-box">
          <Input
            autoFocus={true}
            value={reply}
            placeholder="Reply..."
            onChange={(e) => {
              setReply(e.target.value);
            }}
          />
        </div>
      </form>
    </div>
  );
};

export default Thread;
