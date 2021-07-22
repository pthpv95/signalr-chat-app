import React, { useState } from 'react';
import Avatar from '../shared/avatar';
import Input from '../shared/input';
import Messages from '../shared/messages';
import Channels from './channels';
import DirectMessage from './direct-message';

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
