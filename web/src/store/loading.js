// state
const state = () => ({
  isLoading: false,
})

// getters
const getters = {
  getLoadingState: (state) => state.isLoading,
}

// actions
const actions = {
  showLoading({ commit }) {
    commit("showLoading")
  },
  hideLoading({ commit }) {
    commit("hideLoading")
  },
}

// mutations
const mutations = {
  showLoading(state) {
    state.isLoading = true
  },

  hideLoading(state) {
    state.isLoading = false
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
