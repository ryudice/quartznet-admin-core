import {combineReducers} from "redux";
import jobs from './jobsReducer';

const rootReducer = combineReducers({
    jobs
});

export default rootReducer;