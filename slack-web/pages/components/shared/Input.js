import React from 'react';

const Input = ({ value, onChange, ...props }) => {
  return (
    <input
      className="input-chat"
      value={value}
      type="text"
      placeholder="Jot something down"
      onChange={onChange}
      {...props}
    />
  );
};

export default Input;
