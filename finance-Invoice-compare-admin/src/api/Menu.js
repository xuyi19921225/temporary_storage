import request from '@/utils/request'

export function getMenusByRoleID(params) {
  return request({
    url: '/RoleMenu',
    method: 'get',
    params
  })
}
