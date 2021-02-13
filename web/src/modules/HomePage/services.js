import { getAsync, BASE_URL, postAsync } from "../../services/HttpClient";

const getFriendSuggestions = async () => {
  const url = BASE_URL + `contacts/suggestions`;
  return getAsync(url);
}

const addContact = async (contactId) => {
  const url = BASE_URL + 'contacts';
  return await postAsync(url, {
    receiverId: contactId
  })
}

const approveContactRequest = async id => {
  const url = BASE_URL + `contacts/${id}/accept-friend-request`
  return await postAsync(url, {
    requestId: id
  })
}

const getContactRequests = async () => {
  const url = BASE_URL + `contacts/requests`
  return getAsync(url)
}


export {
  getFriendSuggestions,
  getContactRequests,
  approveContactRequest,
  addContact
}