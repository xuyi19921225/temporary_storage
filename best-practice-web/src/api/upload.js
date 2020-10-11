import request from '@/utils/request'

export function fileUpload(data) {
  return request({
    headers: {
      'content-type': 'multipart/form-data'
    },
    url: '/file',
    method: 'post',
    data
  })
}
