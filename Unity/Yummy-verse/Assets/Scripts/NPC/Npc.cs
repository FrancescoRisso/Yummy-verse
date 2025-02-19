using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public abstract class NPC : MonoBehaviour {
	[Header("Audio")]
	[SerializeField]
	private AudioSource audioSource;

	protected abstract AudioClip GetNthAudioClip(int n);
	protected abstract void RunNthAnimation(int n);
	protected virtual int NumInteractions() {
		return 0;
	}
	protected virtual void Assertions() {}

	private int _interaction_num = 0;

	protected Animator _animator;

	protected GameObject _player;

	protected CharacterController _controller;

	protected bool _is_walking = false;

	protected Transform _dest;

	protected float _move_speed_npc;

	protected float _rotation_speed_npc;

	void Start() {
		Assertions();

		if(NumInteractions() != 0) Assert.IsNotNull(audioSource, $"{name} does not have an audio source assigned");
		for(int i = 0; i < NumInteractions(); i++) Assert.IsNotNull(GetNthAudioClip(i), $"{name} does not have the {i}-th audio clip assigned");

		_animator = GetComponent<Animator>();
		Assert.IsNotNull(_animator, $"{name} cannot find an animator assigned");

		_controller = GetComponent<CharacterController>();
		Assert.IsNotNull(_animator, $"{name} cannot find a character controller");

		_player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(_player, $"{name} cannot find the player");
	}

	public void NextAnimation() {
		AudioClip next_audio = GetNthAudioClip(_interaction_num);
		if(next_audio != null) {
			audioSource.clip = next_audio;
			audioSource.Play();
			RunNthAnimation(_interaction_num);
		}

		_interaction_num++;
	}

	protected bool IsSpeaking() {
		return audioSource.isPlaying;
	}

	private void WalkTowards(Transform wp) {
		Vector3 targetPos = new Vector3(wp.position.x, transform.position.y, wp.position.z);
		Vector3 direction = targetPos - transform.position;
		float distance = direction.magnitude;

		if(distance > 0.05) {
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotation_speed_npc * Time.deltaTime);

			Vector3 dir = transform.forward;
			dir.y = 0;
			_controller.Move(Time.deltaTime * _move_speed_npc * dir);
		} else {
			_is_walking = false;
		}
	}

	void Update() {
		if(_is_walking) WalkTowards(_dest);
	}
}
