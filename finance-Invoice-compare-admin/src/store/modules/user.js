import { login } from '@/api/login'
import { getUserInfoByToken } from '@/api/user'
import { getMenusByRoleID } from '@/api/menu'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'

const getDefaultState = () => {
  return {
    token: getToken(),
    name: '',
    avatar: '',
    roles: [],
    company: '',
    asyncRoutes: []
  }
}

const state = getDefaultState()

const mutations = {
  RESET_STATE: (state) => {
    Object.assign(state, getDefaultState())
  },
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  },
  SET_ROLEID: (state, roleID) => {
    state.roleID = roleID
  },
  SET_COMPANY: (state, company) => {
    state.company = company
  },
  SET_ASYNCROUTES: (state, routes) => {
    state.asyncRoutes = routes
  }
}

const actions = {
  // user login
  login({ commit }, loginInfo) {
    return new Promise((resolve, reject) => {
      login(loginInfo).then(res => {
        try {
          if (res.success === true) {
            const data = JSON.parse(res.response)
            commit('SET_TOKEN', data.auth_token)
            setToken(data.auth_token)
            resolve()
          } else {
            reject(res.message)
          }
        } catch (error) {
          reject(error)
        }
      }).catch(error => {
        reject(error)
      })
    })
  },

  // get user info
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {
      getUserInfoByToken({ token: state.token }).then(res => {
        try {
          if (res.success === true) {
            const data = res.response
            commit('SET_NAME', data.userName)
            commit('SET_ROLES', data.roleName)
            commit('SET_ROLEID', data.roleID)
            commit('SET_COMPANY', data.companyCode)
            commit('SET_AVATAR', '')
            resolve()
          } else {
            reject(res.message)
          }
        } catch (error) {
          reject(error)
        }
      }).catch(error => {
        reject(error)
      })
    })
  },

  getMenu({ commit, state }) {
    return new Promise((resolve, reject) => {
      getMenusByRoleID({ roleId: state.roleID }).then(res => {
        try {
          if (res.success === true) {
            const data = res.response
            commit('SET_ASYNCROUTES', data)
            resolve(data)
          } else {
            reject(res.message)
          }
        } catch (error) {
          reject(error)
        }
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({ commit }) {
    return new Promise((resolve) => {
      removeToken() // must remove  token  first
      resetRouter()
      commit('RESET_STATE')
      resolve()
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      removeToken() // must remove  token  first
      commit('RESET_STATE')
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

