import React from 'react';

const Channels = () => {
  const channels = [
    {
      "id": "9",
      "name": "Boyer, Wisozk and Brakus"
    },
    {
      "id": "x",
      "name": "Thompson, Hagenes and Jacobson"
    },
    {
      "id": "o",
      "name": "Ferry, Swaniawski and Conroy"
    },
    {
      "id": "0",
      "name": "Blick - Wolff"
    },
    {
      "id": "s",
      "name": "Volkman - Ankunding"
    },
    {
      "id": "m",
      "name": "Zulauf - Wolf"
    }
  ]
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