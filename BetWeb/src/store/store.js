import { createStore } from 'vuex'

// Create a new store instance.
let store = createStore({
    state () {
      return {
        count: 0
      }
    },
    mutations: {
      increment (state) {
        state.count++
      }
    }
  })

  export default store;