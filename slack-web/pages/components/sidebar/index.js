import faker from 'faker';
import React, { useState } from 'react';
import Channels from './channels';
import DirectMessage from './direct-message';

const users = [
  {
    id: faker.random.alphaNumeric(5),
    "name": "Julian Schowalter",
    "avatar": "avatar1"
  },
  {
    id: faker.random.alphaNumeric(5),
    "name": "Beulah Kuhlman",
    "avatar": "avatar2"
  },
  {
    id: faker.random.alphaNumeric(5),
    "name": "Jana Schuppe",
    "avatar": "avatar3"
  },
  {
    id: faker.random.alphaNumeric(5),
    "name": "Clinton Schmitt",
    "avatar": "avatar4"
  },
  {
    id: faker.random.alphaNumeric(5),
    "name": "Tyler Herzog",
    "avatar": "avatar5"
  }
]

const SideBar = ({ onDirectMessageClick }) => {
  const [reply, setReply] = useState('');
  return (
    <div className="sidebar-content">
      <Channels />
      <DirectMessage users={users} onClick={(id) => {
        onDirectMessageClick({
          ...users.find(u => u.id === id)
        })
      }} />
    </div>
  );
};

export default SideBar;
