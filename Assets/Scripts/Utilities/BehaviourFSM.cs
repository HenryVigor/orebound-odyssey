using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// BehaviourFSM
/// <summary>
///     <see cref="MonoBehaviour"/> class that acts as a finite state machine.
///     States act as swappable GameObject components with rules for which other
///     state to switch to
/// </summary>
public class BehaviourFSM : MonoBehaviour {
    /// <summary>The currently active state</summary>
    State_Base ActiveState;
    
    /// <summary>Sets the active state</summary>
    /// <param name="stateType">The type of the state to set</param>
    protected void SetState(Type stateType) {
        if (stateType != null) {
            // Disable current state
            if (ActiveState != null) ActiveState.enabled = false;
            
            // Create new state if not created already and set as active
            State_Base nextState =
                (State_Base)gameObject.GetComponent(stateType)
            ;
            if (nextState == null) {
                ActiveState = (State_Base)gameObject.AddComponent(stateType);
                ActiveState.MachineBase = this;
            } else {
                ActiveState = nextState;
                ActiveState.enabled = true;
            }
        }
    }
    
    /// <summary>Base class for all state types</summary>
    public abstract class State_Base : MonoBehaviour {
        /// <summary>Pointer to parent machine's derivation</summary>
        /// <value>To be set by parent machine on state creation</value>
        public abstract BehaviourFSM MachineBase { protected get; set; }
        
        void LateUpdate() {
            MachineBase.SetState(Transitions());
            OnLateUpdate();
        }
        
        /// On Late Update
        /// <summary>
        ///     Replacement for <see cref="LateUpdate"/> in derived state
        ///     instances
        /// </summary>
        protected virtual void OnLateUpdate() {}
        
        /// Transition Rules
        /// <summary>
        ///     Rules for state transitions. Checked at the end of every frame.
        ///     The state will change to the returned type. If null is returned,
        ///     state will not change
        /// </summary>
        /// <returns>The type of the state to switch to</returns>
        protected virtual Type Transitions() { return null; }
        
    }
    
    /// <summary>Default derivable state type</summary>
    public class State : State_Base {
        BehaviourFSM _MachineBase;
        /// <summary>Pointer to the parent machine's base class</summary>
        /// <value>
        ///     Set only if null.<br/>
        ///     To be set by parent machine on state creation
        /// </value>
        public sealed override BehaviourFSM MachineBase {
            protected get => _MachineBase;
            set => _MachineBase ??= value;
        }
    }
    
    /// <summary>Generic state with pointer to derived parent machine</summary>
    /// <typeparam name="T">Parent machine derived type</typeparam>
    public class State<T> : State_Base where T : BehaviourFSM {
        T _Machine;
        /// <summary>Pointer to the parent machine's derived class</summary>
        /// <value>
        ///     Set only if null<br/>
        ///     To be set by <see cref="MachineBase"/> on state creation
        /// </value>
        protected T Machine {
            get => _Machine;
            set => _Machine ??= value;
        }
        
        BehaviourFSM _MachineBase;
        /// <summary>Pointer to the parent machine's base class</summary>
        /// <value>
        ///     Sets self and <see cref="Machine"/> only if null<br/>
        ///     To be set by parent machine on state creation
        /// </value>
        public sealed override BehaviourFSM MachineBase {
            protected get => _MachineBase;
            set {
                if (_MachineBase == null) {
                    _MachineBase = value;
                    Machine = (T)_MachineBase;
                }
            }
        }
    }
}