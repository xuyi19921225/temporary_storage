import { login } from '@/api/login'
import { getUserRoleMenuInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'
import { decode } from '@/utils/jwt'

const getDefaultState = () => {
  return {
    token: getToken(),
    name: '',
    avatar: '',
    roles: [],
    siteID: '',
    divisionID: '',
    department: '',
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
  SET_SITEID: (state, siteID) => {
    state.siteID = siteID
  },
  SET_DIVISIONID: (state, divisionID) => {
    state.divisionID = divisionID
  },
  SET_DEPARTMENT: (state, department) => {
    state.department = department
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
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
      var token = decode(state.token)
      getUserRoleMenuInfo({ ntid: token.nameid, siteID: token.SiteID }).then(res => {
        try {
          if (res.success === true) {
            const data = res.response
            commit('SET_SITEID', data.siteID)
            commit('SET_DIVISIONID', data.divisionID)
            commit('SET_DEPARTMENT', data.department)
            commit('SET_NAME', data.userName)
            commit('SET_ROLES', data.roleName)
            commit('SET_ASYNCROUTES', data.menuList)
            commit('SET_AVATAR', '')
            resolve(data.menuList)
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

