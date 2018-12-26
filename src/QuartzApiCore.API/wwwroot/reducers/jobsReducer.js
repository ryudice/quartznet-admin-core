import {SET_JOBS} from "../actions";

const jobs = (state = [], action) =>{
    switch (action.type) {
        case SET_JOBS:
            return action.data;
        default:
            return state;
    }
}

export default jobs;