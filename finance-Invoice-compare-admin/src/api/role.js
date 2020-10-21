import request from '@/utils/request'

export function getRoleList(params) {
  return request({
    url: '/role',
    method: 'get',
    params
  })
}

export function getAllRoleList(params) {
  return request({
    url: '/role/getAllRoleList',
    method: 'get',
    params
  })
}

export function addRole(data) {
  return request({
    url: '/role',
    method: 'post',
    data
  })
}

export function saveRole(data) {
  return request({
    url: '/role',
    method: 'put',
    data
  })
}

export function deleteRole(params) {
  return request({
    url: '/role',
    method: 'delete',
    params
  })
}

