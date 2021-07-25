import React from 'react';
import Image from 'next/image'

const DirectMessage = ({ conversations, onClick }) => {
  return (
    <div className="sidebar-content__direct-message">
      <p className="sidebar-content__direct-message--title">Direct messages</p>
      {conversations && conversations.map((u, idx) => {
        return (<div key={u.title} className="sidebar-content__direct-message--user" onClick={() => onClick(u.id)}>
          <Image src={`/assets/avatar1.jpeg`} alt={u.name} width={30} height={30} />
          <p>{u.title}</p>
        </div>)
      })}
    </div>
  );
};

export default DirectMessage;