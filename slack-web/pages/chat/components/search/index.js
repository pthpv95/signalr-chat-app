import Image from 'next/image';
import { useRouter } from 'next/router';
import React from 'react';
import useUser from '../../../../hooks/auth/useUser';

const Search = ({ thread, onSubmit, onMoreAction, onCloseThread }) => {
  const { isLoading, data, isError } = useUser();
  const router = useRouter()
  if(isError) {
    router.push('/login')
  }
  if(isLoading) {
    return 'Loading...'
  }

  if(!data){
    return null
  }

  return (
    <div className="search__user-info">
      <Image src={`/assets/avatar1.jpeg`} alt={data.firstName} width={30} height={30} />
      <p>{data.firstName + ' ' + data.lastName}</p>
    </div>
  );
};

export default Search;