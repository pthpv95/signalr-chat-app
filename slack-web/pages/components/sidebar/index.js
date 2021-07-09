import React, { useState } from 'react';
import Avatar from '../shared/Avatar';
import Input from '../shared/Input';
import Messages from '../shared/Messages';
import Channels from './Channels';
import DirectMessage from './DirectMessage';

const Search = ({ thread, onSubmit, onMoreAction, onCloseThread }) => {
  const [reply, setReply] = useState('');
  return (
    <div className="sidebar-content">
      <Channels />
      <DirectMessage />
    </div>
  );
};

export default Search;
