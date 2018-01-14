namespace MoenenGames.VoxelRobot {
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
    using UnityEngine.UI;


	[DisallowMultipleComponent]
	public abstract class StageObject : MonoBehaviour {





		#region --- VAR ----


		// Shot Cut
		public bool IsAlive {
			get {
				return HP > 0f || TheHealth.Invincible;
			}
		}


		// Setting
		public BasicInfo Info = new BasicInfo();
		public Health TheHealth = new Health();
		public Breakable TheBreakable = new Breakable();

        public RectTransform BattleLog;
        

		// Data
		public float HP {
			get;
			protected set;
		}



		#endregion




		#region --- EDT ---


#if UNITY_EDITOR

		protected virtual void Reset () {
			Info.Reset(transform);
			TheHealth.Reset();
			TheBreakable.Reset(transform);
		}

#endif

		#endregion




		#region --- MSG ---



		protected virtual void Awake () {
			HP = TheHealth.FullHP;
            
        }


		protected virtual void Start () {
            BattleLog = GameObject.FindGameObjectWithTag("BattleLog").GetComponent<RectTransform>();
            
        }



		protected virtual void Update () {
            
        }



		protected virtual void OnDied () {

			// Sys
			HP = 0f;

			// Breakable
			TheBreakable.PlayParticles();
			TheBreakable.BlowOut(transform);

			// Destroy
			if (TheBreakable.DestroyOnDied) {
				Destroy(gameObject);
			} else {
				gameObject.SetActive(false);
			}

		}




		#endregion




		#region --- API ---



		public virtual void TakeDamage (float damage, DamageType type = DamageType.Physics, StageObject shooter = null) {

			// Check
			if ((shooter && shooter.Info.Team == Info.Team) || !IsAlive) {
				return;
			}

			// Logic
			if (!TheHealth.Invincible) {
				HP -= damage * TheHealth.GetFixedProtect(type);
                    
                if (BattleLog)
                {
                    BattleLog.sizeDelta = new Vector2(0, BattleLog.rect.height + 11);
                    BattleLog.GetComponent<Text>().text = BattleLog.GetComponent<Text>().text + "  " + shooter.Info.Name + " dealt " + (damage * TheHealth.GetFixedProtect(type)) + " " + type + " damage to " + this.name + "\n";
                }

                if (!IsAlive) {
					// Msg
					OnDied();
				}
			}

		}



		public void SetControllable (bool ctrllable) {
			Controllable[] cs = GetComponentsInChildren<Controllable>(true);
			for (int i = 0; i < cs.Length; i++) {
				cs[i].Active = ctrllable;
			}
		}



		public void SwitchToEdittime () {
			SetControllable(false);
		}


		public void SwitchToRuntime () {
			SetControllable(true);
		}



		#endregion



	}
}