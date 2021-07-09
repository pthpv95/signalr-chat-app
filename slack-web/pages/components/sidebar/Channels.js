import React from 'react';
import faker from 'faker';

const Channels = () => {
  const channels = new Array(6).fill(0).map((_, i) => {
    return {
      id: faker.random.alphaNumeric(),
      name: faker.company.companyName()
    }
  });

  return (
    <div className="sidebar-content__channel">
      <p className="sidebar-content__channel--title">Channels</p>
      {channels.map(u => {
        return <div className="sidebar-content__channel--item" key={u.id}>
          <p>#</p>
          <p>{u.name}</p>
        </div>
      })}
    </div>
  );
};

export default Channels;