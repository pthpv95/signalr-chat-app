import { fetchWrapper } from "../../hooks/fetchWrapper";

const getUserConversation = () => {
  return fetchWrapper('/messages/conversations', 'GET')
}

export {
  getUserConversation
}