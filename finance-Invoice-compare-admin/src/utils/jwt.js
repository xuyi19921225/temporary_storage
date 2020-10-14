import jwt from 'jsonwebtoken'

export function decode(value) {
  return jwt.decode(value)
}

export function encode(value) {
  return jwt.en(value)
}
