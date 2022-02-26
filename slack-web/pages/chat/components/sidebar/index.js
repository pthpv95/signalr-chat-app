import React, { useEffect, useState } from 'react';
import useQueryUserConversations from '../../../../hooks/chat/useQueryUserConversations';
import Channels from './channels';
import DirectMessage from './direct-message';

const SideBar = ({ onDirectMessageClick }) => {
  const { data, isLoading } = useQueryUserConversations();
  return (
    <div className="sidebar-content">
      <Channels />
      <DirectMessage conversations={data} onClick={(id) => {
        onDirectMessageClick({
          ...users.find(u => u.id === id)
        })
      }} />
    </div>
  );
};

export default SideBar;
