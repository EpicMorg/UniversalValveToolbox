cmake_minimum_required(VERSION 3.1)
project(retime_phoneme)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES main.cpp)
add_executable(retime_phoneme ${SOURCE_FILES})