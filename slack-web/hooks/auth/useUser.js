import { useQuery } from "react-query";
import { fetchWrapper } from "../fetchWrapper";

const useUser = () => {
  const fetchUser = () => {
    // eslint-disable-next-line react-hooks/rules-of-hooks
    return fetchWrapper('/users/me', 'GET').then((user) => {
      return user;
    }).catch((e) => {
      throw e;
    })
  }

  return useQuery('user_info', () => fetchUser())
}

export default useUser;