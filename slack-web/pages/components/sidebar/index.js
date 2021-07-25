import React, { useState } from 'react';
import { users } from '../../../config';
import useUser from '../../hooks/useUser';
import Channels from './channels';
import DirectMessage from './direct-message';

const SideBar = ({ onDirectMessageClick }) => {
  // const [reply, setReply] = useState('');
  const { isLoading, data, error } = useUser();
  if(isLoading) {
    return 'Loading...'
  }

  if(!data){
    return null
  }

  // const directMessages = users.find(u => u.id === userId).contacts;
  return (
    <div className="sidebar-content">
      <Channels />
      <DirectMessage users={[]} onClick={(id) => {
        onDirectMessageClick({
          ...users.find(u => u.id === id)
        })
      }} />
    </div>
  );
};

export default SideBar;
