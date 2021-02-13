import { getAsync, postAsync, BASE_URL } from "../../services/HttpClient"

const getContacts = async () => {
  const url = BASE_URL + `contacts/user`
  return getAsync(url)
}

const getConversationInfo = async (input) => {
  const url = BASE_URL + `api/messages/contact`;
  return postAsync(url, input)
}

export { getContacts, getConversationInfo }