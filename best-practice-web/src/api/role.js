import request from '@/utils/request'

export function getRoleList(params) {
  return request({
    url: '/Role',
    method: 'get',
    params
  })
}

