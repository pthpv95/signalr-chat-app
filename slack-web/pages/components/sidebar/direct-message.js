import React from 'react';

import Image from 'next/image'

const DirectMessage = ({ users, onClick }) => {
  return (
    <div className="sidebar-content__direct-message">
      <p className="sidebar-content__direct-message--title">Direct messages</p>
      {users && users.map((u, idx) => {
        return (<div key={u.name} className="sidebar-content__direct-message--user" onClick={() => onClick(u.id)}>
          <Image src={`/assets/avatar${idx+1}.jpeg`} alt={u.name} width={30} height={30} />
          <p>{u.name}</p>
        </div>)
      })}
    </div>
  );
};

export default DirectMessage;