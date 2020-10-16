import request from '@/utils/request'

// 获取Token 信息
export function login(params) {
  return request({
    url: '/login',
    method: 'get',
    params
  })
}

