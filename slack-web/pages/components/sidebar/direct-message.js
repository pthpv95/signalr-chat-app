import React from 'react';
import faker from 'faker';
import Image from 'next/image'

const DirectMessage = () => {
  // const users = new Array(10).fill(0).map((_, i) => {
  //   return {
  //     name: faker.name.findName(),
  //     avatar: faker.image.avatar()
  //   }
  // });

  const users = [
    {
      "name": "Julian Schowalter",
      "avatar": "https://cdn.fakercloud.com/avatars/malykhinv_128.jpg"
    },
    {
      "name": "Beulah Kuhlman",
      "avatar": "https://cdn.fakercloud.com/avatars/ganserene_128.jpg"
    },
    {
      "name": "Jana Schuppe",
      "avatar": "https://cdn.fakercloud.com/avatars/sta1ex_128.jpg"
    },
    {
      "name": "Clinton Schmitt",
      "avatar": "https://cdn.fakercloud.com/avatars/shoaib253_128.jpg"
    },
    {
      "name": "Tyler Herzog",
      "avatar": "https://cdn.fakercloud.com/avatars/miguelkooreman_128.jpg"
    }
  ]
  return (
    <div className="sidebar-content__direct-message">
      <p className="sidebar-content__direct-message--title">Direct messages</p>
      {users.map(u => {
        return (<div key={u.name} className="sidebar-content__direct-message--user">
          <Image src="/assets/avatar.jpg" alt={u.name} width={30} height={30}/>
          <p>{u.name}</p>
        </div>)
      })}
    </div>
  );
};

export default DirectMessage;