/* eslint-disable react-hooks/rules-of-hooks */
import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import { QueryClient } from 'react-query';
import { fetchWrapper } from '../hooks/fetchWrapper';

const queryClient = new QueryClient();
export default function Layout() {
  const router = useRouter()
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setIsLoading(true)
    fetchWrapper('/users/me', 'GET').then((user) => {
      setIsLoading(false)
      queryClient.setQueryData('user_info', user);
      if (!user) {
        router.push('/login')
      } else {
        router.push(router.asPath)
      }
    })
  }, [router])

  return (
    <div>
      {isLoading ? 'Loading ...' : ''}
    </div>
  );
}
