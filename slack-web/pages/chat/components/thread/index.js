import React, { useState } from 'react';
import Avatar from '../shared/avatar';
import Input from '../shared/input';
import Messages from '../shared/messages';

const Thread = ({ thread, onSubmit, onMoreAction, onCloseThread }) => {
  const [reply, setReply] = useState('');
  if(!thread){
    return <></>
  }
  return (
    <div className="thread">
      <div className="thread__heading">
        <h4>Thread</h4>
        <button className="btn btn-unstyled" onClick={onCloseThread}>X</button>
      </div>
      <div className="thread__sub-heading">
        <Avatar />
        <div className="thread__sub-heading--text">
          <p className="thread__sub-heading--owner">{thread.createdBy}</p>
          <p className="thread__sub-heading--">{thread.title}</p>
        </div>
      </div>
      {thread.replies.length > 0 &&
        <div className="thread__num-replies">
          <span>{thread.replies.length} replies</span>
        </div>
      }
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
