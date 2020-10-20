import { constantRoutes } from '@/router'

// 添加路由映射
const map = {
  layout: () => import('@/layout'),
  user: () => import('@/views/system/user/index'),
  role: () => import('@/views/system/role/index'),
  menu: () => import('@/views/system/menu/index')
}

const state = {
  routes: [],
  asyncRoutes: []
}

const mutations = {
  SET_ROUTES: (state, asyncRoutes) => {
    state.asyncRoutes = asyncRoutes
    state.routes = constantRoutes.concat(asyncRoutes)
  }
}

const actions = {
  generateRoutes({ commit }, menuList) {
    return new Promise(resolve => {
      try {
        var asyncRoutes = []
        if (menuList && menuList.length > 0) {
          var parentMenuList = menuList.filter((value) => value.parentID === -1)
          // 修改数据结构
          parentMenuList.forEach(parentElement => {
            var parentTemp = {
              path: parentElement.path,
              name: parentElement.menuName,
              component: map[parentElement.component],
              hidden: parentElement.hidden,
              redirect: parentElement.redirect,
              alwaysShow: parentElement.alwaysShow,
              meta: {
                title: parentElement.title,
                icon: parentElement.icon
              }
            }

            var childrenMenuList = menuList.filter((value) => value.parentID === parentElement.id)
            var childrenTemp = []
            var temp = {}
            childrenMenuList.forEach(childrenElement => {
              temp = {
                path: childrenElement.path,
                name: childrenElement.menuName,
                component: map[childrenElement.component],
                hidden: childrenElement.hidden,
                alwaysShow: childrenElement.alwaysShow,
                meta: {
                  title: childrenElement.title,
                  icon: childrenElement.icon
                }
              }
              childrenTemp.push(temp)
            })

            parentTemp.children = childrenTemp

            asyncRoutes.push(parentTemp)
          })
        }
        asyncRoutes.push({ path: '*', redirect: '/404', hidden: true })
        commit('SET_ROUTES', asyncRoutes)
        resolve()
      } catch (error) {
        console.log(error)
      }
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
