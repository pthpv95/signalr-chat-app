import React from 'react';
import Image from 'next/image'
const Avatar = () => {
  return (
    <Image
      width={40}
      height={40}
      src={"https://ca.slack-edge.com/TEGSLQ86L-U01JM86M3HN-1e6b3d3e7eaf-512"}
      alt="tada"
      layout="fixed"
    />
  );
};

export default Avatar;