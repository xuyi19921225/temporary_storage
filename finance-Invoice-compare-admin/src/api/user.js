import request from '@/utils/request'

export function getUserRoleMenuInfo(params) {
  return request({
    url: '/User/GetUserRoleMenuInfo',
    method: 'get',
    params
  })
}

export function getUserInfoByToken(params) {
  return request({
    url: '/User/GetUserInfoByToken',
    method: 'get',
    params
  })
}

export function getUserList(params) {
  return request({
    url: '/User',
    method: 'get',
    params
  })
}

export function addUser(data) {
  return request({
    url: '/User',
    method: 'post',
    data
  })
}

export function saveUser(data) {
  return request({
    url: '/User',
    method: 'put',
    data
  })
}

export function deleteUser(params) {
  return request({
    url: '/User',
    method: 'delete',
    params
  })
}
