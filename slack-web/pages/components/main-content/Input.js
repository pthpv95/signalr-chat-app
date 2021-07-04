import React from 'react';

const Input = ({ value, onChange }) => {
  return (
    <input
      className="input-chat"
      value={value}
      type="text"
      placeholder="Jot something down"
      onChange={onChange}
    />
  );
};

export default Input;
