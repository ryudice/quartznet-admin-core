import {SET_JOBS, SET_TRIGGERS} from "./actions";
import axios from 'axios';
const makeActionCreator = (type, ...argNames) =>{
    return (...args)=> {
        const action = { type }
        argNames.forEach((arg, index) => {
            action[argNames[index]] = args[index]
        });
        return action;
    }
};

export const setJobs = makeActionCreator(SET_JOBS, 'data');
export const setTriggers = makeActionCreator(SET_TRIGGERS, 'data');

export const fetchJobs = (dispatch) => {
    return axios.get("/api/jobs").then((response)=>{
        dispatch(setJobs(response.data));  
    });
}

export const fetchTriggers = (dispatch) => {
    return axios.get("/api/triggers").then((response)=>{
        dispatch(setTriggers(response.data));
    });
}

export const pauseJob = (job) =>{
    return (dispatch, getState) =>{
        axios.post(`/api/jobs/${job.group}/${job.name}/pause`).then((response)=>{
            alert('Job was triggered')
        }).catch((error) => alert('There was a problem triggering the job'));
    }
}

export const triggerJob = (job) =>{
    return (dispatch, getState) =>{
        axios.post(`/api/jobs/${job.group}/${job.name}/trigger`).then((response)=>{
                alert('Job was triggered')
        }).catch((error) =>  alert('There was a problem triggering the job'));
    }
}