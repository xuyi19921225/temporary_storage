import request from '@/utils/request'

export function getMenusByRoleID(params) {
  return request({
    url: '/RoleMenu',
    method: 'get',
    params
  })
}

export function getMenus(params) {
  return request({
    url: '/menu',
    method: 'get',
    params
  })
}
